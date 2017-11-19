using System.Linq;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HordeFlow.HR.Infrastructure.Stores
{
    public class GroupStoreBase
    {
        public DbContext Context { get; set; }
        public DbSet<Group> DbEntitySet { get; private set; }

        public IQueryable<Group> EntitySet
        {
            get 
            {
                return this.DbEntitySet;
            }
        }

        public GroupStoreBase(DbContext context)
        {
            this.Context = context;
            this.DbEntitySet = context.Set<Group>();
        }

        public void Create(Group entity)
        {
            this.DbEntitySet.Add(entity);
        }

        public void Delete(Group entity)
        {
            this.DbEntitySet.Remove(entity);
        }

        public virtual Task<Group> GetByIdAsync(object id)
        {
            return this.DbEntitySet.FindAsync(new object[] { id });   
        }

        public virtual Group GetById(object id)
        {
            return this.DbEntitySet.Find(new object[] { id });
        }

        public virtual void Update(Group entity)
        {
            if(entity != null)
            {
                this.Context.Entry<Group>(entity).State = EntityState.Modified;
            }
        }
    }   
}