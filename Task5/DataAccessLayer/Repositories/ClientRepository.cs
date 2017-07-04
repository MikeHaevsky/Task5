using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private SalesSaverDBContext db;

        public ClientRepository(SalesSaverDBContext context)
        {
            db = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients.ToList();
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public void Create(Client item)
        {
            db.Clients.Add(item);
        }

        public void Update(Client item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Client item)
        {
            db.Clients.Remove(item);
        }
    }
}