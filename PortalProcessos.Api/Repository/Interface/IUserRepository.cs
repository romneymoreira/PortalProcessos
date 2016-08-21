using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Repository.Interface
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        IEnumerable<User> GetUsers();
        void SaveUser(User user);
        void DeleteUser(int userId);
    }
}
