using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Client> Clients { get; }
        IRepository<Manager> Managers { get; }
        IRepository<Product> Products { get; }
        IRepository<Operation> Operations { get; }
        Task SaveAsync();
    }
}
