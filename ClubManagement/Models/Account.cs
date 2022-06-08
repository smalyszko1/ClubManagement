using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public string Role { get; set;  }
    }
}