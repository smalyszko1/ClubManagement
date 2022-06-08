using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Wymagane pole")]
        public string Nick { get; set; }
        [Required(ErrorMessage = "Wymagane pole")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Hasło się nie zgadza, spróbuj jeszcze raz!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}