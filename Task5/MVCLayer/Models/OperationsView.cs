using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Models
{
    public class OperationsView
    {
        public IEnumerable<OperationView> SomeOperations { get; set; }
        public DateTime LowDate { get; set; }
        public DateTime HighDate { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Clients { get; set; }
        public SelectList Products { get; set; }
        public int LowCost { get; set; }
        public int HighCost { get; set; }
        public bool MatchesNotFound { get; set; }
    }
}