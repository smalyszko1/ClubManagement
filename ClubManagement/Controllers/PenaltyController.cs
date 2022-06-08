using ClubManagement.DAL;
using ClubManagement.Models;
using ClubManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class PenaltyController : Controller
    {
        // GET: Penalty
        [Authorize]
        public ActionResult List(int? value)
        {
            ClubContext mDb = new ClubContext();
            int paid=0, unpaid=0;
            List<Penalty> penaltiesList = mDb.Penalties.ToList();
            var penaltiesPaid = penaltiesList.Where(x => x.Status == true);
            var penaltiesUnpaid= penaltiesList.Where(x => x.Status == false);
            //sss
            foreach (var item in penaltiesList)
            {
                if (item.Status) paid = paid + item.Value;
                else unpaid = unpaid + item.Value;
            }
            ViewBag.Unpaid = unpaid;
            ViewBag.Paid = paid;
            if(value==0) return View(penaltiesPaid);
            else if(value==1) return View(penaltiesUnpaid);
            return View(penaltiesList);

        }
        
        public ActionResult Create()
        {
            ClubContext mDb = new ClubContext();
            List<Player> playersList = mDb.Players.ToList();
            var viewModel = new CreatePenaltyVM()
            {
                Players = playersList,
        };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Penalty penalty)

        {
            if (!ModelState.IsValid) return View("Create", penalty);
            
            else
            {
                ClubContext db = new ClubContext();
                Player player = new Player();
                string name;
                db.Players.Attach(player);
                player = db.Players.Find(Convert.ToInt32(penalty.Player));
                name = player.Name + " " + player.Surname;
                Penalty p = new Penalty
                {
                    Id = penalty.Id,
                    Type = penalty.Type,
                    Status = false,
                    Player = name,
                    Description = penalty.Description,
                    Value=penalty.Value,
                    

                };

               
                db.Penalties.Add(p);
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int id)
        {
            ClubContext db = new ClubContext();
            Penalty penalty = new Penalty();
            db.Penalties.Attach(penalty);
            penalty = db.Penalties.Find(id);
            db.Penalties.Remove(penalty);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        
        public ActionResult Set ( int id)
        {
            ClubContext db = new ClubContext();
            Penalty penalty = new Penalty();
            db.Penalties.Attach(penalty);
            penalty = db.Penalties.Find(id);
            if(penalty.Status==true)
            penalty.Status = false;
            else penalty.Status = true;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}