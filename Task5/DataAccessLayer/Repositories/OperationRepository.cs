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
    public class OperationRepository : IRepository<Operation>
    {
        private SalesSaverDBContext db;

        public OperationRepository(SalesSaverDBContext context)
        {
            db = context;
        }

        public IEnumerable<Operation> GetAll()
        {
            return db.Operations;
        }

        public Operation Get(int id)
        {
            return db.Operations.Find(id);
        }

        public void Create(Operation item)
        {
            db.Operations.Add(item);
        }

        public void Update(Operation item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Operation item)
        {
            db.Operations.Remove(item);
        }
    }
}
