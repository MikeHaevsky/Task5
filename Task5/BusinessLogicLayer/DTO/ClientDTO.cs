using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        //public virtual ICollection<OperationDTO> Operations { get; set; }
    }
}
