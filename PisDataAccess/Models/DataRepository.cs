using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Objects;

using PisDataAccess;

namespace PISServer.Models
{
    public interface IDataRepository<T>
    {
        void Save();
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        string Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        protected readonly DevelopmentPISEntities _context;

        public DataRepository(DevelopmentPISEntities context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            return query;
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public string Insert(T entity)
        {
            T result = _context.Set<T>().Add(entity);
            return result.ToString();
        }

        public void Update(T entity)
        {
            DbEntityEntry entityEntry = _context.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
                entityEntry.State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
