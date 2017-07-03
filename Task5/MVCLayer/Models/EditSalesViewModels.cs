using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Models
{
    public class EditManagerViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
    }

    public class EditClientViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditOperationViewModel
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public SelectList ManagerList { get; set; }
        public int Manager { get; set; }
        public SelectList ClientList { get; set; }
        public int Client { get; set; }
        public SelectList ProductList { get; set; }
        public int Product { get; set; }
        public int Cost { get; set; }
        public bool NotFound { get; set; }
    }
}