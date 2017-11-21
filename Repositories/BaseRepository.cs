using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HordeFlow.HR.Repositories.Interfaces;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Utils;
using System.Text;
using System.IO;

namespace HordeFlow.HR.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity, new()
    {
        private HrContext context;

        public HrContext Context
        {
            get
            {
                return this.context;
            }
        }

        HrContext IRepository<T>.Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BaseRepository(HrContext context)
        {
            this.context = context;
        }

        public async Task InsertAsync(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            await context.Set<T>().AddAsync(entity);
        }

        /// Saves the changes to database and handles concurrency conflicts.
        public async Task Commit()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("This records has been changed by another user. Error details: " + ex.Message);
            }
        }

        public virtual async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).AnyAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await context.Set<T>().FirstAsync(x => x.Id == id);
        }

        public async Task<T> SeekAsync(int id)
        {
            return await context.Set<T>().AsNoTracking().FirstAsync(x => x.Id == id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstAsync(predicate);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstAsync();
        }

        public async Task<IResponseData<T>> List()
        {
            var query = context.Set<T>().AsNoTracking();
            var response = new ResponseData<T>
            {
                data = await query.ToListAsync(),
                total = await query.CountAsync()
            };
            return response;
        }

        public async Task<ResponseSearchData> SearchAsync(int? currentPage = 1, int? pageSize = 100, string filter = "", string sort = "", string fields = "")
        {
            IQueryable<T> query = context.Set<T>();
            var response = new ResponseSearchData
            {
                data = null,
                total = 0,
                pageSize = pageSize,
                pageCount = 0,
                currentPage = currentPage
            };
            if (string.IsNullOrEmpty(sort))
                query = query.OrderBy(x => x.Id);
            else
                query = ExpressionBuilder.OrderBy(query, sort);

            if (string.IsNullOrEmpty(filter))
            {
                var data = query.AsNoTracking();
                response.total = await data.CountAsync();
                response.pageCount = (int)Math.Ceiling((double)response.total / (int)pageSize);
                if (string.IsNullOrEmpty(fields))
                    response.data = await data.Skip(((int)currentPage - 1) * (int)pageSize).Take((int)pageSize).ToListAsync();
                else
                {
                    // This is the Serializer approach that uses Reflection, accurate but very slow. 
                    // Takes about 300ms when making an http request
                    // var projected = await data.Skip(((int)currentPage - 1) * (int)pageSize).Take((int)pageSize).Select((T x) =>
                    //    JObject.Parse(JsonConvert.SerializeObject(x, Formatting.Indented,
                    //     new JsonSerializerSettings { ContractResolver = new FieldSerializer(fields) }))).ToListAsync();

                    // This approach by manually loading the correct json properties takes only 30ms.
                    var projected = await data
                        .Skip(((int)currentPage - 1) * (int)pageSize)
                        .Take((int)pageSize)
                        .Select((T x) => Serialize(x, fields)).ToListAsync();

                    response = new ResponseSearchData
                    {
                        total = response.total,
                        pageSize = response.pageSize,
                        pageCount = response.pageCount,
                        currentPage = currentPage,
                        data = projected
                    };
                }
            }
            else
            {
                var predicate = ExpressionBuilder.BuildFilterExpression<T>(filter);
                var data = query.AsNoTracking().Where(predicate);
                response.total = await data.CountAsync();
                response.pageCount = (int)Math.Ceiling((double)response.total / (int)pageSize);
                if (string.IsNullOrEmpty(fields))
                    response.data = await data.Skip(((int)currentPage - 1) * (int)pageSize).Take((int)pageSize).ToListAsync();
                else
                {
                    // This is the Serializer approach that uses Reflection, accurate but very slow. 
                    // Takes about 300ms when making an http request
                    // var projected = await data.Skip(((int)currentPage - 1) * (int)pageSize).Take((int)pageSize).Select((T x) =>
                    //     JObject.Parse(JsonConvert.SerializeObject(x, Formatting.Indented,
                    //     new JsonSerializerSettings { ContractResolver = new FieldSerializer(fields) }))).ToListAsync();
                    var projected = await data.Skip(((int)currentPage - 1) * (int)pageSize).Take((int)pageSize).Select((T x) =>
                        Serialize(x, fields)).ToListAsync();
                    response = new ResponseSearchData
                    {
                        total = response.total,
                        pageSize = response.pageSize,
                        pageCount = response.pageCount,
                        currentPage = currentPage,
                        data = projected
                    };
                }
            }

            return response;
        }

        private JObject Serialize(object data, string fields)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, data);
            }

            var field = fields.Split(',', StringSplitOptions.RemoveEmptyEntries);
            string[] fieldsArray = field.Select(f => ToCamelCase(f.Trim())).ToArray();
            var json = JObject.Parse(sb.ToString());
            var newJson = new JObject();

            foreach (var f in fieldsArray)
            {
                var value = json.GetValue(f);
                newJson.Add(ToLowerCamelCase(f), value);
            }
            return newJson;
        }

        public string ToLowerCamelCase(string input)
        {
            string[] words = input.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach (string s in words)
            {
                string firstLetter = s.Substring(0, 1);
                string rest = s.Substring(1, s.Length - 1);
                sb.Append(firstLetter.ToLower() + rest);
                sb.Append(' ');

            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }

        public string ToCamelCase(string input)
        {
            string[] words = input.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach (string s in words)
            {
                string firstLetter = s.Substring(0, 1);
                string rest = s.Substring(1, s.Length - 1);
                sb.Append(firstLetter.ToUpper() + rest);
                sb.Append(' ');

            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public void Update(T entity)
        {
            // EntityEntry dbEntityEntry = context.Entry<T>(entity);
            // dbEntityEntry.State = EntityState.Modified;
            context.Update(entity);
        }

        public void Delete(T entity)
        {
            // EntityEntry dbEntityEntry = context.Entry<T>(entity);
            // dbEntityEntry.State = EntityState.Deleted;
            context.Remove(entity);
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }
    }
}