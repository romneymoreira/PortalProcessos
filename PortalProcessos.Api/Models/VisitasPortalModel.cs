using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Models
{
    public class VisitasPortalModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime DataVisita { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }
}
