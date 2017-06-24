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
    public class ProductRepository : IRepository<Product>
    {
        private SalesSaverDBContext db;

        public ProductRepository(SalesSaverDBContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product Get(int id)
        {
            return db.Products.FirstOrDefault(item => item.Id == id);
        }

        public void Create(Product item)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == item.Id);
            if (product == null)
                db.Products.Add(item);
        }

        public void Update(Product item)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == item.Id);
            if (product != null)
                db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
                db.Products.Remove(product);
        }
    }
}
