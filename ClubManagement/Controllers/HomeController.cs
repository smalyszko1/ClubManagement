using ClubManagement.DAL;
using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ClubManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MainPage()
        {
            int paid = 0, unpaid = 0;
            ClubContext db = new ClubContext();
            int size = db.Players.ToList().Count;
            List<Penalty> penaltiesList = db.Penalties.ToList();
            List<Event> eventsList = db.Events.OrderByDescending(n => n.Day).ToList();
            List<News> news = db.News.OrderByDescending(n => n.Posted).Take(1).ToList();
            List<Event> trainings = db.Events.OrderBy(s => s.Day).Where(s => s.EventName.ToString() == "trening").ToList();
            List<Event> matches = db.Events.OrderBy(s => s.Day).Where(s => s.EventName.ToString() == "mecz").ToList();
            
            Opponent opponent = new Opponent();

            Match lastMatch = new Match();
            List<Match> matchesLast = db.Matches.OrderByDescending(s => s.Day).ToList();
            lastMatch = matchesLast[0];
             Opponent lastOpponent = db.Opponents.Find(lastMatch.OpponentID);
           
            ViewBag.LastMatchDay = lastMatch.Day.ToShortDateString() + " " +lastMatch.Time.ToShortTimeString();
            ViewBag.LastMatchScored = lastMatch.GoalsScored;
            ViewBag.LastMatchLost = lastMatch.GoalsLost;
            ViewBag.LastMatchImg = lastOpponent.ImgSource;
            ViewBag.LastMatchOpponent = lastMatch.Opponent;
            ViewBag.LastMatchID = lastMatch.Id;
            if(trainings.Count()>0)
            {
                Event nextTraining = trainings[0];
                ViewBag.TrainingDay = nextTraining.Day;
                ViewBag.TrainingHour = nextTraining.Day.ToShortDateString() + " " + nextTraining.Time.ToShortTimeString();
                ViewBag.TrainingPLace = nextTraining.EventPlace;
                ViewBag.TrainingId = nextTraining.Id;
                ViewBag.IsTraining = true;
            }
            else
            {
                ViewBag.IsTraining = false;
            }
        

            
            if (matches.Count()>0)
            {
                Event nextMatch = matches[0];
                Opponent nextOpponent = db.Opponents.Find(nextMatch.OpponentID);
                opponent = db.Opponents.Find(nextMatch.OpponentID);
                ViewBag.NextMatchOpponentID = nextOpponent.Id;
                ViewBag.MatchDay = nextMatch.Day.ToShortDateString() +" "+ nextMatch.Time.ToShortTimeString();
                //ViewBag.MatchHour = nextMatch.Time.TimeOfDay;
                if (nextMatch.Away) ViewBag.MatchPlace = "Wyjazd";
                else ViewBag.MatchPlace = "U siebie";
                ViewBag.Img = nextOpponent.ImgSource;
                ViewBag.MatchOpponent = nextOpponent.Name;
                ViewBag.MatchId = nextMatch.Id;
                ViewBag.IsMatch = true;
            }
            else
            {
                ViewBag.IsMatch = false;
                ViewBag.NextMatchOpponentID = "-";
                ViewBag.MatchDay = "-";
                ViewBag.MatchHour = "-";
                
                ViewBag.MatchPlace = "-";
                ViewBag.Img = "-";
                ViewBag.MatchOpponent = "-";
                ViewBag.MatchId = "-";
            }
            
            var penaltiesPaid = penaltiesList.Where(x => x.Status == true);
            var penaltiesUnpaid = penaltiesList.Where(x => x.Status == false);
            //sss
            foreach( var item in eventsList)
            {
                DateTime check = item.Day.Date + item.Time.TimeOfDay;
                if(check<DateTime.Now)
                {
                    if (item.EventName.ToString() == "trening")
                    {
                        Training t = new Training
                        {
                            Id = item.Id,
                            EventName = item.EventName.ToString(),
                            EventPlace = item.EventPlace.ToString(),
                            Time = item.Time,
                            Day = item.Day,
                            Info = item.Info,
                            Updated = false,
                        };
                        db.Events.Attach(item);

                        db.Events.Remove(item);

                        db.Trainings.Add(t);
                        db.SaveChanges();
                    }
                    else if (item.EventName.ToString() == "mecz")
                    {
                        Match m = new Match
                        {
                            Id = item.Id,
                            //Opponent = item.AwayPlace,
                            Time = item.Time,
                            Day = item.Day,
                            OpponentIMG=item.OpponentIMG,
                            Info = item.Info,
                            Updated = false,
                            AddedPLayersToMatch = false,
                            OpponentID=item.OpponentID,
                            Opponent=item.Opponent,

                        };
                        db.Events.Attach(item);

                        db.Events.Remove(item);

                        db.Matches.Add(m);
                        db.SaveChanges();
                    }
                }
            }
            foreach (var item in penaltiesList)
            {
                if (item.Status) paid = paid + item.Value;
                else unpaid = unpaid + item.Value;
            }
            ViewBag.Unpaid = unpaid;
            ViewBag.Paid = paid;
            ViewBag.Size = size;
            ViewBag.Content = news[0].Contents;
            ViewBag.Title = news[0].Title;
            ViewBag.Posted = news[0].Posted;
            //Response.Write("<script>alert('Data inserted successfully')</script>");
            return View();
        }

      
    }
}