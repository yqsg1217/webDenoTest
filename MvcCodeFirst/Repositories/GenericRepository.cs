using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcCodeFirst.DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MvcCodeFirst.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal AccountContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(AccountContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAsNoTrcking()
        {
            return dbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        //public void Save()
        //{
        //    context.SaveChanges();
        //}

        //private bool disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}