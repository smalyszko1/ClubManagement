
using ClubManagement.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClubManagement.DAL
{
    public class ClubContext : DbContext
    {
        public ClubContext() : base("Club")
        { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Account> Accounts { get; set;  }
        public DbSet<Role> Roles { get; set;  }
        public DbSet<Event> Events { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Opponent> Opponents { get; set; }
        public DbSet<PlayerToMatch> PlayerToMatches { get; set; }
        
    }
}