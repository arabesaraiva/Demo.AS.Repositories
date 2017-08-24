using Demo.AS.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories
{

    public class DemoContext : DbContext
    {

        public DemoContext() 
            : base("name=ConnectionStringKeyName")
        {

        }

        public DemoContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //CONVENTIONS - REMOVING
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //CONFIGURATIONS
            modelBuilder.Properties<DateTime>().Configure(p => p.HasColumnType("datetime2"));
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }

    }

}
