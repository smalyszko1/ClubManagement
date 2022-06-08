using ClubManagement.DAL;
using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static ClubManagement.Models.Event;

namespace ClubManagement.Controllers
{
    public class EventController : Controller
    {
        // GET: Calendar
        List<DateTime> dateList = new List<DateTime>();
        List<DateTime> timeList = new List<DateTime>();
        [Authorize]
        public ActionResult List()
        {
            ClubContext db = new ClubContext();
            
            var events =
            from e in db.Events
            select e;

            events = events.OrderBy(s=>s.Day);
            return View(events.ToList());
        }
        public ActionResult CreateSoloTraining()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSoloTraining(Event eventToView)
        {
            if (!ModelState.IsValid) return View("CreateSoloTraining", eventToView);
            else
            {
                
                Event e = new Event
                {

                    Day = eventToView.Day,
                    Time= eventToView.Time,
                    Id= eventToView.Id,
                    EventName=EventType.trening,
                    EventPlace=eventToView.EventPlace,
                    Opponent="-",
                    Info=eventToView.Info,


                };
                ClubContext db = new ClubContext();
                db.Events.Add(e);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public ActionResult CreateMatch()
        {
            ClubContext db = new ClubContext();
            IEnumerable<Opponent> opponents = db.Opponents.ToList();
            ViewBag.Opponents = opponents;
            return View();
        }
        [HttpPost]
        public ActionResult CreateMatch(Event eventToView)
        {
          
            if (!ModelState.IsValid) return View("CreateMatch", eventToView);
            else
            {
                ClubContext db = new ClubContext();
                Opponent opponent = new Opponent();
                opponent = db.Opponents.Find(eventToView.OpponentID);
                Event e = new Event
                {
                    Day = eventToView.Day,
                    Time = eventToView.Time,
                    Id = eventToView.Id,
                    EventName = EventType.mecz,
                    EventPlace = Place.inne,
                    Opponent =opponent.Name,
                    OpponentIMG=opponent.ImgSource,
                    Info = eventToView.Info,
                    OpponentID=eventToView.OpponentID,
                    Away=eventToView.Away,

                };
                
                db.Events.Add(e);
                db.SaveChanges();
            }
            
            return RedirectToAction("List");
        }
        public ActionResult CreateMultiTraining()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult CreateMultiTraining(Event eventToView,FormCollection collection)
        {
            if (!ModelState.IsValid) return View("CreateMatch", eventToView);
            else if(Convert.ToInt64(collection["warun"])==0)
            {
                DateTime valueDa = Convert.ToDateTime(collection["Day"]);
                TimeSpan valueTi = Convert.ToDateTime(collection["Time"]).TimeOfDay;
                DateTime result = valueDa + valueTi;
                dateList.Add(result);
                return View("CreateMatch", eventToView);
            }
            else
            {

                DateTime valueDa = Convert.ToDateTime(collection["Day"]);
                TimeSpan valueTi = Convert.ToDateTime(collection["Time"]).TimeOfDay;
                DateTime result = valueDa + valueTi;
               
                foreach (DateTime data in dateList)
                {
                    Event e = new Event
                    {
                        EventPlace = eventToView.EventPlace,
                        Info = eventToView.Info,
                        Day = data.Date,
                        Time = data.Date,


                    };
                    ClubContext db = new ClubContext();
                    db.Events.Add(e);
                    db.SaveChanges();
                }
                dateList = null;
            }
            
            return RedirectToAction("List");
        }
        public void AddList(FormCollection collection)
        {
           
            
           
        }
        public ActionResult Delete(int id)
        {
            ClubContext db = new ClubContext();
            Event e = new Event();
            e = db.Events.Find(id);
            db.Events.Remove(e);
            db.SaveChanges();
           
            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            ClubContext db = new ClubContext();
            Event e= new Event();
            db.Events.Attach(e);
            e = db.Events.Find(id);
            Event eToView = new Event
            {
                Day = e.Day,
                Time = e.Time,
                EventPlace = e.EventPlace,
                Info = e.Info,
            };
            return View(eToView);
        }
        [HttpPost]
        public ActionResult Edit(Event eToView, int id)
        {
            ClubContext db = new ClubContext();
            Event e = new Event();
            db.Events.Attach(e);
            e = db.Events.Find(id);
            e.AwayPlace = eToView.AwayPlace;
            e.Day = eToView.Day;
            e.EventPlace = eToView.EventPlace;
            e.Info = eToView.Info;
            e.Time = eToView.Time;
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");

        }
    }
    }
