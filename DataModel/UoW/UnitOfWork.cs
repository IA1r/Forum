using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.UoW;
using DataModel.Entity;
using System.Data.Entity;
using Core.Interfaces.DbContext;

namespace DataModel.UoW
{
    public class UnitOfWork :  IUnitOfWork
    {
        private Forum_dbEntities context;
        private bool isDisposed;

        public UnitOfWork(IForumtDbContext context)
        {
            this.context = context as Forum_dbEntities;
        }

        public DbSet<T> Set<T>() where T : class
        {
            return this.context.Set<T>();
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.isDisposed = true;
        }


        public int Save()
        {
            return this.context.SaveChanges();
        }

        public EntityState Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return this.context.Entry(entity).State = EntityState.Modified;
        }
    }
}
