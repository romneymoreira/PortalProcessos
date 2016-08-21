using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Domain
{
    public class AcessosPortal : DomainObject
    {
        public int IdAcesso { get; set; }
        public int IdUser { get; set; }
        public DateTime DataAcesso { get; set; }
        public virtual User Usuario { get; set; }

        private AcessosPortal() { }
    }
}
