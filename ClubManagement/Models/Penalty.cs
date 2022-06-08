using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Penalty
    {
        [Key]
        public int Id{get; set;}

        public PenaltyType Type { get; set; }
        
        public string Player { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }

        public string Color
        {
            get
            {
                if (!Status) return "red";
                else return "green";
            }
        }
        public string StatusText
        {
            get
            {
                if (!Status) return "nieopłacono";
                else return "opłacono";
            }
        }
         public int Value
        {

            get;


            set;
        }

        public enum PenaltyType
        {
            spóźnienie,
            nieobecność,
            nieprzygotowanie,
            inne,
        };
    }
}