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
            return db.Managers.Find(id);
        }

        public void Create(Manager item)
        {
            db.Managers.Add(item);
        }

        public void Update(Manager item)
        {
            db.Entry<Manager>(item).State = EntityState.Modified;
        }

        public void Delete(Manager item)
        {
            db.Managers.Remove(item);
        }
    }
}
