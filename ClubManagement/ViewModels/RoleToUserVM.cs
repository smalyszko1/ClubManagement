using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.ViewModels
{
    public class RoleToUserVM
    {
        [Key]
        public int Id { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}