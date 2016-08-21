using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool UsarCriptografia { get; set; }
        public bool UsarSenhaPadrao { get; set; }
        public int LevelValue { get; set; }
        public int StatusValue { get; set; }
        public string Level
        {
            get
            {
                switch (this.LevelValue)
                {
                    case 1:
                        return "User";
                    case 2:
                        return "Super User";
                    case 3:
                        return "Manager";
                    case 4:
                        return "Admin";
                    case 5:
                        return "SysAdmin";
                    default:
                        return "!INDEFINIDO!";

                }
            }
        }
        public string Status
        {
            get
            {
                switch (this.StatusValue)
                {
                    case 0:
                        return "Inativo";
                    case 1:
                        return "Ativo";
                    case 2:
                        return "Pendente Ativação";
                    default:
                        return "!INDEFINIDO!";
                }
            }
        }

    }
}
