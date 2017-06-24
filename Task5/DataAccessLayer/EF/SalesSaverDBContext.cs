using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    public partial class SalesSaverDBContext : DbContext
    {
        static SalesSaverDBContext()
        {
            Database.SetInitializer<SalesSaverDBContext>(null);
        }

        public SalesSaverDBContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientMap());
            modelBuilder.Configurations.Add(new ManagerMap());
            modelBuilder.Configurations.Add(new OperationMap());
            modelBuilder.Configurations.Add(new ProductMap());
        }
    }
}
