using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Models
{
    public class EditManagerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter nickname")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Length must be on 3-15 symbols")]
        public string Nickname { get; set; }
    }

    public class EditClientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter nickname")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Length must be on 3-15 symbols")]
        public string Nickname { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter nameproduct")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Length must be on 3-15 symbols")]
        public string Name { get; set; }
    }

    public class EditOperationViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public System.DateTime Date { get; set; }
        public SelectList ManagerList { get; set; }
        public int ManagerId { get; set; }
        public SelectList ClientList { get; set; }
        public int ClientId { get; set; }
        public SelectList ProductList { get; set; }
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Need more gold")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Cost { get; set; }
        public bool NotFound { get; set; }
    }
}