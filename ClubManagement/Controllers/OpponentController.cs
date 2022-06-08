using ClubManagement.DAL;
using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class OpponentController : Controller
    {
        // GET: Opponent
        [Authorize]
        public ActionResult Index()
        {
            ClubContext db = new ClubContext();
            List<Opponent> opponents = db.Opponents.ToList();
            return View(opponents);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Opponent opponentFromView, HttpPostedFileBase file)
        {

            string path;
       
            if (!ModelState.IsValid) return View("Create", opponentFromView);
            else
            {
                Opponent opponent = new Opponent
                {
                    Id = opponentFromView.Id,
                    Name = opponentFromView.Name,
                    LostGoals=0,
                    ScoredGoals=0,
                    Loses=0,
                    Wins=0,
                    Draws=0,
                };

                

                    

                   
                        path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        opponent.ImgSource = file.FileName;
                    
                    
             
              
                

                ClubContext db = new ClubContext();
                db.Opponents.Add(opponent);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            ClubContext db = new ClubContext();
            Opponent op = new Opponent();
            db.Opponents.Attach(op);
            op = db.Opponents.Find(id);
            db.Opponents.Remove(op);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNew(Opponent opponentFromView, HttpPostedFileBase file)
        {

            string path;

            if (!ModelState.IsValid) return View("Create", opponentFromView);
            else
            {
                Opponent opponent = new Opponent
                {
                    Id = opponentFromView.Id,
                    Name = opponentFromView.Name,
                    LostGoals = 0,
                    ScoredGoals = 0,
                    Loses = 0,
                    Wins = 0,
                    Draws = 0,
                };



               


                path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                opponent.ImgSource = file.FileName;






                ClubContext db = new ClubContext();
                db.Opponents.Add(opponent);
                db.SaveChanges();
            }

            return RedirectToAction("CreateMatch","Event");
        }
        public ActionResult Details (int id)
        {
            ClubContext db = new ClubContext();
            Opponent opponent = db.Opponents.Find(id);
            ViewBag.IMG = opponent.ImgSource;
            return View(opponent);
        }

    }
}