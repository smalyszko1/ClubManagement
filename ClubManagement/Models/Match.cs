using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class Match
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh/mm } ", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        public string Opponent { get; set; }
        public int OpponentID { get; set; }
        public List<DateTime> DateList { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsLost { get; set; }
        public List<Player> Players { get; set; }
        public string PlayersString { get; set; }
        public string IdPlayers { get; set; }
        public string IdPlayersToMatch { get; set; }
        public string Info { get; set; }
        public string OpponentIMG { get; set; }
        public bool Updated { get; set; }
        public bool AddedPLayersToMatch { get; set; }
        public string ScoreColor
        {
            get
            {

                if (GoalsScored>GoalsLost) return "lawngreen";
                else if (GoalsScored == GoalsLost) return "white";
                else return "red";
            }
        }

    }
}