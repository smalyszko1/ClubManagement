using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public string EventName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh/mm } ", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }        
        public string EventPlace { get; set; }
        public string Info { get; set; }
        public List<Player> Players { get; set; }
        public List<Player> AbsentPlayers { get; set; }
        public string AbsentPlayersString { get; set; }
        public bool Updated { get; set;  }

    }
}