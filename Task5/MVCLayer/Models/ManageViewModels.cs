using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Models
{
    public class CreateUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }
        public SelectList Roles { get; set; } 
    }
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        public string Address { get; set; }

        //[Required]
        public string Name { get; set; }
        public string Role { get; set; }
        public SelectList Roles { get; set; }
    }
}