using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Models
{
    public class AcessosPortalModel
    {
        public int IdAcesso { get; set; }
        public int IdUser { get; set; }
        public DateTime DataAcesso { get; set; }
    }
}
