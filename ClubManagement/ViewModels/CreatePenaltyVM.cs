using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubManagement.ViewModels
{
    public class CreatePenaltyVM
    {
        public IEnumerable<Player> Players { get; set; }
        public string Player { get; set; }
        public PenaltyType Type { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public enum PenaltyType
        {
            spóźnienie,
            nieobecność,
            nieprzygotowanie,
            inne,
        };
    }
}