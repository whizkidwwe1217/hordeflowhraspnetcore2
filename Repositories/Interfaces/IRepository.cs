using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity, new()
    {
        HrContext Context { get; set; }
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IResponseData<T>> List();
        Task<int> Count();
        Task<T> Get(int id);
        Task<T> Seek(int id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<ResponseSearchData> Search(int? currentPage = 1, int? pageSize = 100, string filter = "", string sort = "", string fields = "");
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        Task Commit();
    }
}