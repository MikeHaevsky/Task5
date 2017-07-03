using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Infrastructure
{
    public class OperationDetails : Exception
    {
        public bool Succedeed { get; set; }
        public string Property { get; set; }
        public OperationDetails(bool succeded ,string message, string prop)
            : base(message)
        {
            Succedeed = succeded;
            Property = prop;
        }
    }
}
