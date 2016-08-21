using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Mappings
{
    public class LinksPortalConfiguration : EntityTypeConfiguration<LinksPortal>
    {
        public LinksPortalConfiguration()
        {
            this.HasKey(p => p.IdLink);

            this.Property(p => p.UrlLink)
                .IsRequired()
                .HasMaxLength(300);
            this.Property(p => p.NomeLink)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(p => p.DataInclusao)
                .IsRequired();
            this.HasRequired(p => p.Setor)
                .WithMany()
                .HasForeignKey(p => p.IdSetor);
        }
    }
}
