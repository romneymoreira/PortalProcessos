using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Models
{
    public class LinksPortalModel
    {
        public int IdLink { get; set; }
        public int IdSetor { get; set; }
        public string NomeSetor { get; set; }
        public string UrlLink { get; set; }
        public string NomeLink { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
