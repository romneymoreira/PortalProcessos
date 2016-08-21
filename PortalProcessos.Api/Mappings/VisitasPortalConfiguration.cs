using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PortalProcessos.Api.Mappings
{
    public class VisitasPortalConfiguration : EntityTypeConfiguration<VisitasPortal>
    {
        public VisitasPortalConfiguration()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.DataVisita)
                .IsRequired();

            this.HasRequired(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUser);
        }
    }
}
