using ClubManagement.DAL;
using ClubManagement.Models;
using ClubManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class MatchController : Controller
    {
        // GET: Match
        [Authorize]
        public ActionResult List()
        {
            ClubContext db = new ClubContext();

            var matches =
            from e in db.Matches
            select e;

            matches = matches.OrderByDescending(s => s.Day);
            

            return View(matches.ToList());
        }
        public ActionResult Delete(int id)
        {
            ClubContext db = new ClubContext();
            Match match = new Match();
            db.Matches.Attach(match);
            match = db.Matches.Find(id);
            db.Matches.Remove(match);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult AddPlayers(int id)
        {
            ClubContext db = new ClubContext();
            Match match = new Match();
            db.Matches.Attach(match);
            match = db.Matches.Find(id);
            ViewBag.Name = match.Opponent;
            ViewBag.Day = match.Day.Date;
            List<Player> playersList = db.Players.ToList();
            List<string> playersListString = new List<string>();

            Match m = new Match()
            {

                Players = playersList,

            };

            return View(m);
        }
        [HttpPost]
        public ActionResult AddPlayers(Training training, int id, FormCollection colletion)
        {
            var b = colletion["Players"].ToString();
           
            b.ToArray();
            ClubContext db = new ClubContext();
            Match match = new Match();
            List<Player> matchPlayers = new List<Player>();
            string[] splitted = b.Split(',');
            int[] ids = new int[splitted.Length];

            for (int i = 0; i < splitted.Length; i++)
            {
                ids[i] = int.Parse(splitted[i]);
            }
            for (int i=0; i<ids.Count(); i++)
            {
                matchPlayers.Add(db.Players.Find(ids[i]));
            }
            
            string lista = "";
            string name = "";
            Player player = new Player();
            foreach (var item in matchPlayers)
            {
                db.Players.Attach(item);
                player = db.Players.Find(Convert.ToInt32(item.id));
                player.Games++;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                name = player.Name + " " + player.Surname;
                lista = lista + name + ", ";
            }

            Match match1 = new Match();
            db.Matches.Attach(match1);
            match1 = db.Matches.Find(id);
            match1.IdPlayers =  string.Join(",",ids); 
            match1.Updated = true;
            match1.PlayersString = lista;
            db.Entry(match1).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("List");
        }
        public ActionResult Details(int id)
        {
            ClubContext db = new ClubContext();

            Match m = new Match();
            db.Matches.Attach(m);
            m = db.Matches.Find(id);

            return View(m);
        }
        public ActionResult AddStats(int id)
        {
            ClubContext db = new ClubContext();
            Match m = new Match();
            db.Matches.Attach(m);
            m = db.Matches.Find(id);
            if (!m.Updated)
            {
                Response.Write("<script>alert('Data inserted successfully')</script>");
                return RedirectToAction("List");
            }
            else
            {
                List<Player> players = new List<Player>();
                List<PlayerToMatch> playersVM = new List<PlayerToMatch>();
                PlayerToMatch pVM;
                List<PlayerToMatch> checkMatch = db.PlayerToMatches.ToList();
                string[] splitted = m.IdPlayers.Split(',');
                int[] ids = new int[splitted.Length];

                for (int i = 0; i < splitted.Length; i++)
                {
                    ids[i] = int.Parse(splitted[i]);
                }

                for (int i = 0; i < ids.Count(); i++)
                {
                    players.Add(db.Players.Find(ids[i]));

                }
                ViewBag.MatchDay = m.Time;
                ViewBag.Opponent = m.Opponent;
                ViewBag.Score = m.GoalsLost.ToString() + ":" + m.GoalsScored.ToString();
                if (!m.AddedPLayersToMatch)
                {

                    foreach (Player item in players)
                    {

                        pVM = new PlayerToMatch
                        {
                            PlayerId = item.id,
                            MatchId = m.Id,
                            Name = item.Name,
                            Surname = item.Surname,
                            YellowCards = 0,
                            RedCards = 0,
                            Goals = 0,
                            Minutes = 0,
                            Rating = 0,
                            Assists = 0,

                        };
                        playersVM.Add(pVM);
                        db.PlayerToMatches.Add(pVM);
                        db.SaveChanges();

                    }

                }

                m.AddedPLayersToMatch = true;
                db.SaveChanges();
                List<PlayerToMatch> playersToView = db.PlayerToMatches.Where(item => item.MatchId == id).OrderByDescending(item => item.Minutes).ToList();
                return View(playersToView);
            }
        }

        public ActionResult Edit(int id)
        {
            ClubContext db = new ClubContext();
            PlayerToMatch p = db.PlayerToMatches.Find(id);
            Match m = db.Matches.Find(p.MatchId);

            ViewBag.Opponent = m.Opponent;
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(PlayerToMatch p,int id)
        {

            ClubContext db = new ClubContext();
            PlayerToMatch player = new PlayerToMatch();
            Player playerToAdd = new Player();
            
            db.PlayerToMatches.Attach(player);
            player = db.PlayerToMatches.Find(id);
            playerToAdd = db.Players.Find(player.PlayerId);
            playerToAdd.YellowCards = playerToAdd.YellowCards + p.YellowCards;
            playerToAdd.RedCards = playerToAdd.RedCards + p.RedCards;
            playerToAdd.Minutes = playerToAdd.Minutes + p.Minutes;
            playerToAdd.Assists = playerToAdd.Assists + p.Assists;
            playerToAdd.RatingSum = playerToAdd.RatingSum + p.Rating;
            db.PlayerToMatches.Attach(player);
            player = db.PlayerToMatches.Find(id);

            player.YellowCards = p.YellowCards;
            player.RedCards = p.RedCards;
            player.Minutes = p.Minutes;
            player.Goals = p.Goals;
            player.Assists = p.Assists;
            player.Rating = p.Rating;

            db.Entry(playerToAdd).State = EntityState.Modified;
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AddStats", new { id = player.MatchId });
        }

        public ActionResult EditMatch (int id)
        {
            ClubContext db = new ClubContext();
            Match m = db.Matches.Find(id);
            return View(m);
        }
        [HttpPost]
        public ActionResult EditMatch (Match m, int id)
        {
            ClubContext db = new ClubContext();
            Match match = db.Matches.Find(id);
            match.GoalsLost = m.GoalsLost;
            match.GoalsScored = m.GoalsScored;
            Opponent oponnent = db.Opponents.Find(match.OpponentID);
            if (m.GoalsScored > m.GoalsLost) oponnent.Wins++;
            else if (m.GoalsScored < m.GoalsLost) oponnent.Loses++;
            else oponnent.Draws++;
            oponnent.LostGoals = oponnent.LostGoals + m.GoalsLost;
            oponnent.ScoredGoals = oponnent.ScoredGoals + m.GoalsScored;
            db.Entry(match).State = EntityState.Modified;
            db.Entry(oponnent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult PlayedMatches ( int id)
        {
            ClubContext db = new ClubContext();
            List<PlayerToMatch> matches = db.PlayerToMatches.Where(s => s.PlayerId == id).ToList();
            return View(matches);

        }
      
    }
}