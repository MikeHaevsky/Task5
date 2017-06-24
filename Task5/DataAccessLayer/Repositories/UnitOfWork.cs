using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        SalesSaverDBContext db;
        private ClientRepository clientRepository;
        private ManagerRepository managerRepository;
        private ProductRepository productRepository;
        private OperationRepository operationRepository;

        public UnitOfWork(string connectionString)
        {
            db = new SalesSaverDBContext(connectionString);
        }

        public IRepository<Entities.Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        public IRepository<Entities.Manager> Managers
        {
            get
            {
                if (managerRepository == null)
                    managerRepository = new ManagerRepository(db);
                return managerRepository;
            }
        }

        public IRepository<Entities.Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }

        public IRepository<Entities.Operation> Operations
        {
            get
            {
                if (operationRepository == null)
                    operationRepository = new OperationRepository(db);
                return operationRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
