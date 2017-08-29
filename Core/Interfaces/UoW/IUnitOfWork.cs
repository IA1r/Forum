using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Core.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        EntityState Entry<TEntity>(TEntity entity) where TEntity : class;
        int Save();
    }
}