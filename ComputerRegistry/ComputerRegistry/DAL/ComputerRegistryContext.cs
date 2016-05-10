using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ComputerRegistry.Models
{
    public class ComputerRegistryContext:DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Workstation> Workstations { get; set; }
        public DbSet<Computer> Computers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }    
    }
}