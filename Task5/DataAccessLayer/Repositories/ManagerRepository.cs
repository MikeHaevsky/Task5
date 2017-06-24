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
    public class ManagerRepository : IRepository<Manager>
    {
        private SalesSaverDBContext db;

        public ManagerRepository(SalesSaverDBContext context)
        {
            db = context;
        }

        public IEnumerable<Manager> GetAll()
        {
            return db.Managers;
        }

        public Manager Get(int id)
        {
            return db.Managers.FirstOrDefault(item => item.Id == id);
        }

        public void Create(Manager item)
        {
            Manager manager = db.Managers.FirstOrDefault(x => x.Id == item.Id);
            if (manager == null)
                db.Managers.Add(item);
        }

        public void Update(Manager item)
        {
            Manager manager = db.Managers.FirstOrDefault(x => x.Id == item.Id);
            if (manager == null)
                db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Manager manager = db.Managers.FirstOrDefault(x => x.Id == id);
            if (manager == null)
                db.Managers.Remove(manager);
        }
    }
}
