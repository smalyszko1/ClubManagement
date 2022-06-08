using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Player
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Wymagane pole")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wymagane pole")]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Wymagane pole")]
        public string Nationality { get; set; }

        public int YellowCards { get; set; }

        public int Games { get; set;  }
        public int RatingSum { get; set; }
        public int RedCards { get; set; }

        [Required(ErrorMessage = "Wymagane pole")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Brithday { get; set; }

        [Required(ErrorMessage = "Wymagane pole")]
        public Pos Position { get; set; }

        public int Goals { get; set; }

        
        public int Assists { get; set; }

        public int Minutes { get; set; }

        public int Absence { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        public string ImgSource { get; set; }
        public double AvgRating { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Examination { get; set; }

        public enum Pos
        {
            Bramkarz,
            Obronca,
            Pomocnik,
            Napastnik,
        }
        public  string yellowCardsColor
        {
            get
            {

                if (YellowCards % 4 == 3) return "yellow";
                else if (YellowCards % 4 == 0 && YellowCards!=0) return "red";
                else return "";
            }
        }
        public string examColor
        {
            get
            {
                DateTime startTime = DateTime.Now;

                TimeSpan span = Examination.Subtract(startTime);
                if (span.TotalDays < 1) return "red";
                else if (span.TotalDays < 7) return "orange";
                else if (span.TotalDays < 30) return "blue";

                return "black";
            }
        }
        public bool ReadyToPlay
        {
            get
            {
                if (examColor != "red") return true;
                else return false;
            }

        }
    }
}