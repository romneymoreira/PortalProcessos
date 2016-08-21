using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.UnitOfWork
{
    public class UnitOfWork<TContext> : IDisposable, IUnitOfWork<TContext>
         where TContext : DbContext
    {
        private readonly TContext _context;

        public TContext Context
        {
            get { return _context; }
        }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void AddToContext<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void LoadReference<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property)
            where TEntity : class
            where TProperty : class
        {
            _context.Entry<TEntity>(entity).Reference(property).Load();
        }

        public void LoadCollection<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, ICollection<TProperty>>> property)
            where TEntity : class
            where TProperty : class
        {
            _context.Entry<TEntity>(entity).Collection(property).Load();
        }

        public bool SaveChanges()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return false;
        }

        public DbContextTransaction BeginTransation()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();

        }

    }
}
