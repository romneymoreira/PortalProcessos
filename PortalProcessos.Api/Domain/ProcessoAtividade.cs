using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Domain
{
    public class ProcessoAtividade : DomainObject
    {
        public int Id { get; set; }
        public int IdSetor { get; set; }
        public string Atividade { get; set; }
        public int Tipo { get; set; }
        public string Responsavel { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public int Status { get; set; }
        public string Observacao { get; set; }
        public virtual Setores Setor { get; set; }
        public ProcessoAtividade() { }
    }
}
