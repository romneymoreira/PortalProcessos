using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Mappings
{
    public class AcessosPortalConfiguration : EntityTypeConfiguration<AcessosPortal>
    {
        public AcessosPortalConfiguration()
        {
            this.HasKey(p => p.IdAcesso);

            this.Property(p => p.DataAcesso)
                .IsRequired();

            this.HasRequired(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUser);
        }
    }
}
