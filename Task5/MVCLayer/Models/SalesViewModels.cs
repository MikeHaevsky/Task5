using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
    }

    public class ManagerViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OperationViewModel
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

    public class OperationsViewModel
    {
        public IEnumerable<OperationViewModel> SomeOperations { get; set; }
        public int LowCost { get; set; }
        public int HighCost { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Clients { get; set; }
        public SelectList Products { get; set; }
        public bool MatchesNotFound { get; set; }
    }
}