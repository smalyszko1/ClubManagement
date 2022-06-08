using ClubManagement.DAL;
using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class TrainingController : Controller
    {
        // GET: Training
        public ActionResult List()
        {

            ClubContext db = new ClubContext();

            var trainings =
            from e in db.Trainings
            select e;

            trainings = trainings.OrderBy(s => s.Day);
            return View(trainings.ToList());
        }

        public ActionResult Delete(int id)
        {
            ClubContext db = new ClubContext();
            Training training = new Training();
            db.Trainings.Attach(training);
            training = db.Trainings.Find(id);
            db.Trainings.Remove(training);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult AddAbsent(int id)
        {
            ClubContext db = new ClubContext();
            Training training = new Training();
            db.Trainings.Attach(training);
            training= db.Trainings.Find(id);
            
            List<Player> playersList = db.Players.ToList();
            List<string> playersListString= new List<string>();
           
            Training t = new Training()
            {
                
                Players=playersList,

            };

            return View(t);
        }
        [HttpPost]
        public ActionResult AddAbsent(Training training, int id, FormCollection colletion)
        {
            var b = colletion["Players"].ToString();
           
            ClubContext db = new ClubContext();
            Training trainingAdd = new Training();
            List<Player> absentPlayers = new List<Player>();
            string[] splitted = b.Split(',');
            int[] ids = new int[splitted.Length];

            for (int i = 0; i < splitted.Length; i++)
            {
                ids[i] = int.Parse(splitted[i]);
            }
            for (int i = 0; i < ids.Count(); i++)
            {
                absentPlayers.Add(db.Players.Find(ids[i]));
            }
            
            string lista="";
            string name = "";
            Player player = new Player();
            foreach(var item in absentPlayers)
            {
                db.Players.Attach(item);
                player = db.Players.Find(Convert.ToInt32(item.id));
                player.Absence++;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                name = player.Name + " " + player.Surname;
                lista = lista + name +", ";
            }

            Training trrr = new Training();
            db.Trainings.Attach(trrr);
            trrr = db.Trainings.Find(id);
            trrr.Updated = true;
            trrr.AbsentPlayersString = lista;
            db.Entry(trrr).State = EntityState.Modified;
            db.SaveChanges();
           
            
           return RedirectToAction("List");
        }
        public ActionResult Details(int id)
        {
            ClubContext db = new ClubContext();
            Training tr = new Training();
            db.Trainings.Attach(tr);
            tr = db.Trainings.Find(id);
            Training t=new Training
            {
               Id=tr.Id,
               EventName=tr.EventName,
               EventPlace=tr.EventPlace,
               AbsentPlayers=tr.AbsentPlayers,
               AbsentPlayersString=tr.AbsentPlayersString,



            };

            return View(t);
        }

    }
}