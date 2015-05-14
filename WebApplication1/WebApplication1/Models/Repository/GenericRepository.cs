using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Interface;
using System.Linq.Expressions;
using System.Data.Entity;


namespace WebApplication1.Models.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {

        private DbContext _context
        {
            get;
            set;
        }

        public GenericRepository()
            : this(new ControlEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }



        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }

        }

        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }


        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}