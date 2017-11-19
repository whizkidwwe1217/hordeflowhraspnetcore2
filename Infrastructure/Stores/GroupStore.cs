using System;
using System.Linq;
using System.Threading.Tasks;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace HordeFlow.HR.Infrastructure.Stores
{
    public class GroupStore : IDisposable
    {
        private bool _disposed;
        private GroupStoreBase _groupStore;

        public GroupStore(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.Context = context;
            this._groupStore = new GroupStoreBase(context);
        }

        public IQueryable<Group> Groups
        {
            get
            {
                return this._groupStore.EntitySet;
            }
        }

        public DbContext Context { get; private set; }

        public virtual void Create(Group group)
        {
            this.ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");
            this._groupStore.Create(group);
            this.Context.SaveChanges();
        }

        public virtual async Task CreateAsync(Group group)
        {
            this.ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");
            this._groupStore.Create(group);
            await this.Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Group group)
        {
            this.ThrowIfDisposed();
            if (group == null)
                throw new ArgumentException("group");
            this._groupStore.Delete(group);
            await this.Context.SaveChangesAsync();
        }

        public virtual void Delete(Group group)
        {
            this.ThrowIfDisposed();
            if (group == null)
                throw new ArgumentException("group");
            this._groupStore.Delete(group);
            this.Context.SaveChanges();
        }

        public Task<Group> FindByIdAsync(int roleId)
        {
            this.ThrowIfDisposed();
            return this._groupStore.GetByIdAsync(roleId);
        }

        public Group FindById(int roleId)
        {
            this.ThrowIfDisposed();
            return this._groupStore.GetById(roleId);
        }

        public Task<Group> FindByNameAsync(string groupName)
        {
            this.ThrowIfDisposed();
            return this.Context.Set<Group>().Where(e => e.Name.ToUpper() == groupName.ToUpper()).FirstOrDefaultAsync();
        }


        public virtual async Task UpdateAsync(Group group)
        {
            this.ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            this._groupStore.Update(group);
            await this.Context.SaveChangesAsync();
        }


        public virtual void Update(Group group)
        {
            this.ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            this._groupStore.Update(group);
            this.Context.SaveChanges();
        }

        public bool DisposeContext
        {
            get;
            set;
        }


        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.DisposeContext && disposing && this.Context != null)
            {
                this.Context.Dispose();
            }
            this._disposed = true;
            this.Context = null;
            this._groupStore = null;
        }
    }
}