using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Opponent
    {
        [Key]
        public int Id { get; set;  }
        [Required(ErrorMessage = "Wymagane pole")]
        public string Name { get; set;  }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
       
        public string ImgSource { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set;  }
        public int Draws { get; set; }
        public int ScoredGoals { get; set; }
        public int LostGoals { get; set; }
    }
}