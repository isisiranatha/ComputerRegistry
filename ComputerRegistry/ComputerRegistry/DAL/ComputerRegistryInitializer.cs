using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerRegistry.Models;
using System.Data.Entity;

namespace ComputerRegistry.DAL
{
    public class ComputerRegistryInitializer: DropCreateDatabaseIfModelChanges<ComputerRegistryContext>
    {
        protected override void Seed(ComputerRegistryContext context)
        {
            var Makes = new List<Make> 
            { 
                new Make { Description = "Dell"},
                new Make { Description = "HP"},
                new Make { Description = "Acer"}
            };
            
            Makes.ForEach(s => context.Makes.Add(s));
            context.SaveChanges();

            var Workstations = new List<Workstation> 
            { 
                new Workstation {InternalID=2345, DNSName = "nick431",  MakeID = 1,  Model = "T500-23", PrimaryUser = "Nicholas Brooks", PurchasedOn = DateTime.Parse("2011-10-12"), Retire = false } 
            };
            Workstations.ForEach(s => context.Computers.Add(s));
            context.SaveChanges();

            var Servers = new List<Server> 
            { 
                new Server {InternalID=4399, DNSName = "vmhost32",  MakeID = 1,  Model = "3650", Location = "Rack 1", PurchasedOn = DateTime.Parse("2011-10-12"), Retire = false } 
            };
            Servers.ForEach(s => context.Computers.Add(s));
            context.SaveChanges();
        }
    }
}