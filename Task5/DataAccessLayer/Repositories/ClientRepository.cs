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
            return db.Clients;
        }

        public Client Get(int id)
        {
            return db.Clients.FirstOrDefault(item => item.Id == id);
        }

        public void Create(Client item)
        {
            Client client = db.Clients.FirstOrDefault(x => x.Id == item.Id);
            if (client == null)
                db.Clients.Add(item);
        }

        public void Update(Client item)
        {
            Client client = db.Clients.FirstOrDefault(x => x.Id == item.Id);
            if (client != null)
                db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Client client = db.Clients.FirstOrDefault(x => x.Id == id);
            if (client != null)
                db.Clients.Remove(client);
        }
    }
}