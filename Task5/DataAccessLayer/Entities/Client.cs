using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Client
    {
        public Client()
        {
            this.Operations = new List<Operation>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
