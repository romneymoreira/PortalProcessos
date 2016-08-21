using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Mappings
{
    public class ProcessoAtividadeConfiguration : EntityTypeConfiguration<ProcessoAtividade>
    {
        public ProcessoAtividadeConfiguration()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.Observacao)
                .IsRequired()
                .HasMaxLength(5000);

            this.Property(p => p.Atividade)
                .IsRequired()
                .HasMaxLength(5000);

            this.Property(p => p.Responsavel)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(p => p.DataSolicitacao)
                .IsRequired();

            this.Property(p => p.Tipo)
                           .IsRequired();

            this.Property(p => p.Status)
                          .IsRequired();

            this.HasRequired(p => p.Setor).WithMany()
               .HasForeignKey(p => p.IdSetor);

        }
    }
}
