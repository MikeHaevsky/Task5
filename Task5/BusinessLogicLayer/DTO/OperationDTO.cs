using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class OperationDTO
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Cost { get; set; }
        public string ClientNickname { get; set; }
        public string ManagerNickname { get; set; }
        public string ProductName { get; set; }
    }
}
