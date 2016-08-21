using PortalProcessos.Api.Domain;
using PortalProcessos.Api.Repository.Interface;
using PortalProcessos.Api.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Repository
{
    public class UserRepository : RepositoryBase<PortalContext.PortalContext>, IUserRepository
    {
        public UserRepository(IUnitOfWork<PortalContext.PortalContext> unitOfWork)
            : base(unitOfWork)
        {
        }
        public void SaveUser(User user)
        {
            try
            {
                if (user.Id > 0)
                    Context.Update(user);
                else
                    Context.Insert(user);

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var erros in ex.EntityValidationErrors)
                {
                    foreach (var erroVal in erros.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", erroVal.PropertyName, erroVal.ErrorMessage);
                    }
                }
            }
            Context.Save();
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                try
                {
                    Context.Delete(user);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var erros in ex.EntityValidationErrors)
                    {
                        foreach (var erroVal in erros.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", erroVal.PropertyName, erroVal.ErrorMessage);
                        }
                    }
                }

            }
            Context.Save();
        }
        public User GetUser(int userId)
        {
            return Context.Select<User>().FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return Context.GetAll<User>();
        }

       
    }
}
