using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Domain
{
    public class VisitasPortal : DomainObject
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime DataVisita { get; set; }
        public virtual User Usuario { get; set; }

        public VisitasPortal() { }
    }
}
