using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Mappings
{
    public class SetoresConfiguration : EntityTypeConfiguration<Setores>
    {
        public SetoresConfiguration()
        {
            this.HasKey(p => p.IdSetor);

            this.Property(p => p.NomeSetor)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
