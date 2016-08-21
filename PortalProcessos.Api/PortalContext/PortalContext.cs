using Microsoft.AspNet.Identity.EntityFramework;
using PortalProcessos.Api.Domain;
using PortalProcessos.Api.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.PortalContext
{
    public class PortalContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Setores> Setores { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<AcessosPortal> AcessosPortal { get; set; }
        public DbSet<LinksPortal> LinksPortal { get; set; }
        public DbSet<VisitasPortal> VisitasPortal { get; set; }
        public DbSet<ProcessoAtividade> ProcessoAtividade { get; set; }
        public PortalContext()
            : base("PortalContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new SetoresConfiguration());
            modelBuilder.Configurations.Add(new ProcessoAtividadeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new LinksPortalConfiguration());
            modelBuilder.Configurations.Add(new AcessosPortalConfiguration());
            modelBuilder.Configurations.Add(new VisitasPortalConfiguration());
        }
    }
}
