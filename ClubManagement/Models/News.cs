using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubManagement.Models
{
    public class News
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public string Contents { get; set; }
        public string  Author { get; set; }
        public DateTime Posted { get; set; }
        
    }
}