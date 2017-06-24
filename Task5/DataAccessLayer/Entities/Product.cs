using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Product
    {
        public Product()
        {
            this.Operations = new List<Operation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
