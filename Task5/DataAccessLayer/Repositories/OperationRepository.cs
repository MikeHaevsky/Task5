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
            return db.Operations.FirstOrDefault(item => item.Id == id);
        }

        public void Create(Operation item)
        {
            Operation operation = db.Operations.FirstOrDefault(x => x.Id == item.Id);
            if (operation == null)
                db.Operations.Add(item);
        }

        public void Update(Operation item)
        {
            Operation operation = db.Operations.FirstOrDefault(x => x.Id == item.Id);
            if (operation != null)
                db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Operation operation = db.Operations.FirstOrDefault(x => x.Id == id);
            if (operation != null)
                db.Operations.Remove(operation);
        }
    }
}
