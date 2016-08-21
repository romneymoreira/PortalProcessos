using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.PortalContext
{
    /// <summary>
    /// Helper genérico para operações em domain objects
    /// </summary>
    public sealed class DataContext : IDisposable
    {
        private readonly DbContext _context;
        public DataContext(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll<T>() where T : DomainObject
        {
            return _context.Set<T>();
        }


        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> where) where T : DomainObject
        {
            return _context.Set<T>().Where(where);
        }

        public T GetInstanceOrDefault<T>(Expression<Func<T, bool>> where) where T : DomainObject
        {
            return _context.Set<T>().FirstOrDefault(where);
        }

        public T GetInstanceOrDefaultAsNoTracking<T>(Expression<Func<T, bool>> where) where T : DomainObject
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(where);
        }

        public T GetByKey<T>(object key) where T : DomainObject
        {
            return _context.Set<T>().Find(key);
        }

        public IQueryable<T> Select<T>() where T : DomainObject
        {
            return _context.Set<T>();
        }

        public IQueryable<T> Select<T>(params Expression<Func<T, object>>[] includeProperties) where T : DomainObject
        {
            IQueryable<T> query = _context.Set<T>();

            includeProperties.ToList().ForEach(i => query = query.Include(i));

            return query;
        }

        public IQueryable<T> SelectAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : DomainObject
        {

            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            includeProperties.ToList().ForEach(i => query = query.Include(i));

            return query;
        }

        public void Insert<T>(T entity) where T : DomainObject
        {
            Validate(entity);
            _context.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : DomainObject
        {
            Validate(entity);
            DbEntityEntry entityEntry = _context.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
                entityEntry.State = EntityState.Modified;
            }
        }

        public void Delete<T>(T entity) where T : DomainObject
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private void Validate<T>(T entity) where T : DomainObject
        {
            if (entity != null)
                entity.Validate();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
