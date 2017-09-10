using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Models
{
    public class BuscaAtividadeModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Responsavel { get; set; }
        public int IdSetor { get; set; }
        public int Tipo { get; set; }
        public int Status { get; set; }
    }
}
