using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Domain
{
    public class Setores : DomainObject
    {
        public int IdSetor { get; set; }
        public string NomeSetor { get; set; }
        private Setores() { }
    }
}
