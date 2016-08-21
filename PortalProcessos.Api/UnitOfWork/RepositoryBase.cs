using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.UnitOfWork
{
    public abstract class RepositoryBase<TContext> : IDisposable
      where TContext : DbContext
    {
        protected readonly DataContext Context;

        protected IUnitOfWork<TContext> _uow;

        public IUnitOfWork<TContext> UnitOfWork
        {
            get { return _uow; }
        }

        private RepositoryBase()
        {
        }

        protected RepositoryBase(TContext context)
            : this()
        {
            Context = new DataContext(context);
        }

        protected RepositoryBase(IUnitOfWork<TContext> uow)
            : this(uow.Context)
        {
            _uow = uow;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
