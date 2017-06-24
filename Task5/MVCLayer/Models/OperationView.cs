using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLayer.Models
{
    public class OperationView
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