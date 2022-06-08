using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class PlayerToMatch
    {
        [Key]
        public int id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; } 
        public string Name { get; set; }

        
        public string Surname { get; set; }


        public int YellowCards { get; set; }

 
        public int RedCards { get; set; }


    
        public int Goals { get; set; }


        public int Assists { get; set; }

        public int Minutes { get; set; }

        public int Rating { get; set; }


    }
}