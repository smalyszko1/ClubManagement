using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public EventType EventName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh/mm } ", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        public int OpponentID { get; set; }
        public string Opponent { get; set; }
        public List<DateTime> DateList { get; set; }
        public string AwayPlace { get; set; }
        public string OpponentIMG { get; set; }
        public Place EventPlace { get; set; }
        public string Info { get; set; }
        public bool Away { get; set; }
        public string Color
        {
            get
            {
                if (EventName == EventType.mecz) return "#f2f2f2";
                else return "White";
            }
        }
        public enum EventType
        {
            trening,
            mecz,
            inne,
        };

        public enum Place
        {
            orlik,
            hala,
            boisko,
            wyjazd,
            inne,
        };
    }
}