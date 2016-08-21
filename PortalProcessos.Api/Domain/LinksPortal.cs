using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Domain
{
    public class LinksPortal : DomainObject
    {
        public int IdLink { get; set; }
        public int IdSetor { get; set; }
        public string UrlLink { get; set; }
        public string NomeLink { get; set; }
        public DateTime DataInclusao { get; set; }
        public virtual Setores Setor { get; set; }

        //private LinksPortal() { }
        public LinksPortal() { }
    }
}
