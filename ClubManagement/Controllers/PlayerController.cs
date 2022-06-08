using ClubManagement.DAL;
using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class PlayerController : Controller
    {
        // GET: Player
        [Authorize]
        public ActionResult List(int? sortOrder)
        {
           
            ClubContext mDb = new ClubContext();
            var playersList = 
            from p in mDb.Players
            select p;
            
            
            //sss
            //playersList.Sort((p, q) => p.Position.CompareTo(q.Position));
            switch (sortOrder)
            {
                case 1:
                    playersList= playersList.OrderBy(s => s.Brithday);
                    break;
                case 2:
                    playersList = playersList =playersList.OrderBy(s => s.Position);
                    break;
                case 3:
                    playersList = playersList.OrderBy(s => s.Examination);
                    break;
                case 4:
                    playersList = playersList.OrderByDescending(s => s.Goals);
                    break;
                case 5:
                    playersList = playersList.OrderByDescending(s => s.Assists);
                    break;
                case 6:
                    playersList = playersList.OrderByDescending(s => s.Games);
                    break;
                case 7:
                    playersList = playersList.OrderByDescending(s => s.Minutes);
                    break;
                case 8:
                    playersList = playersList.OrderByDescending(s => s.YellowCards);
                    break;
                case 9:
                    playersList = playersList.OrderByDescending(s => s.RedCards);
                    break;
                case 10:
                    playersList = playersList.OrderByDescending(s => s.Absence);
                    break;
                case 11:
                    playersList = playersList.OrderBy(s => s.Surname);
                    break;
                default:
                    playersList=playersList.OrderBy(s => s.Surname);
                    break;
            }
            return View(playersList.ToList());
            
        }
        public ActionResult Create()
        {
          //  var u = Session["rola"] as string;
         //   if (u != "admin") return RedirectToAction("MainPage", "Home");
         //      else
            return View();
        }
        [HttpPost]
        public ActionResult Create(Player player,HttpPostedFileBase file)

        {

            string path;
            if (!ModelState.IsValid) return View("Create", player);
            
                else
            {
                
                Player p = new Player
                {
                    id= player.id,
                    Name = player.Name,
                    Surname = player.Surname,
                    Nationality = player.Nationality,
                    YellowCards = 0,
                    RedCards = 0,
                    Minutes = 0,
                    Goals = 0,
                    Assists = 0,
                    AvgRating=0,
                    Games=0,
                    RatingSum=0,
                    Brithday = player.Brithday,
                    Position = player.Position,
                    Absence = player.Absence,
                    Examination= player.Examination,


                };
                if(file==null) path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName("bezzdjęcia.png"));
                else path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                p.ImgSource = file.FileName;
                ClubContext db = new ClubContext();
                db.Players.Add(p);
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }
        public ActionResult Details(int id)
        {
            ClubContext db = new ClubContext();
            Player player = new Player();
            db.Players.Attach(player);
            player = db.Players.Find(id);
            Player p = new Player
            {
                id = player.id,
                Name = player.Name,
                Surname = player.Surname,
                Nationality = player.Nationality,
                YellowCards = player.YellowCards,
                RedCards = player.RedCards,
                Minutes = player.Minutes,
                Goals = player.Goals,
                Assists = player.Assists,
                Games = player.Games,
                Brithday = player.Brithday,
                Position = player.Position,
                Absence = player.Absence,
                Examination = player.Examination,
                ImgSource=player.ImgSource,
                AvgRating = Math.Round((double)player.RatingSum / (double)player.Games,2),

            };

            return View(p);
        }
        public ActionResult Edit(int id)
        {
            ClubContext db = new ClubContext();
            Player player = new Player();
            db.Players.Attach(player);
            player = db.Players.Find(id);
            Player p = new Player()
            {
                id = player.id,
                Name = player.Name,
                Surname = player.Surname,
                Nationality = player.Nationality,
                YellowCards = player.YellowCards,
                RedCards = player.RedCards,
                Minutes = player.Minutes,
                Goals = player.Goals,
                Assists = player.Assists,
                Games = player.Games,
                Brithday = player.Brithday,
                Position = player.Position,
                Absence = player.Absence,
                Examination = player.Examination,


            };

            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Edit(Player p, int id)
        {
            
            
                ClubContext db = new ClubContext();
                Player player = new Player();
                db.Players.Attach(player);
                player = db.Players.Find(id);
               
                player.YellowCards = p.YellowCards;
                player.RedCards = p.RedCards;
                player.Minutes = p.Minutes;
                player.Goals = p.Goals;
                player.Assists = p.Assists;
                player.Games = p.Games;
                player.Absence = p.Absence;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
            
          //  else return View("Edit", p);
            return RedirectToAction("List");


        }
        public ActionResult Delete(int id)
        {
            ClubContext db = new ClubContext();
            Player player = new Player();
            db.Players.Attach(player);
            player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult EditInfo(int id)
        {
            ClubContext db = new ClubContext();
            Player player = new Player();
            db.Players.Attach(player);
            player = db.Players.Find(id);
            Player p = new Player()
            {
                id = player.id,
                Name = player.Name,
                Surname = player.Surname,
                Nationality = player.Nationality,               
                Brithday = player.Brithday,
                Examination = player.Examination,


            };

            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult EditInfo(Player p, int id)
        {


            ClubContext db = new ClubContext();
            Player player = new Player();
            db.Players.Attach(player);
            player = db.Players.Find(id);

            player.Examination = p.Examination;
            player.Position = p.Position;
            
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();

            //  else return View("Edit", p);
            return RedirectToAction("List");


        }
    }
}