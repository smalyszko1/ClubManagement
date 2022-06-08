using ClubManagement.DAL;
using ClubManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        [Authorize]
        public ActionResult List()
        {
            ClubContext db = new ClubContext();

            List<News> news = db.News.OrderByDescending(n=>n.Posted).ToList();
            
            return View(news);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(News news)
        {
            if (!ModelState.IsValid) return View("Create", news);
            else
            {
                News n = new News
                {
                    Id = news.Id,
                    Title = news.Title,
                    Author = news.Author,
                    Contents = news.Contents,
                    Posted = DateTime.Now,

                };
                ClubContext db = new ClubContext();
                db.News.Add(n);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
        public ActionResult Delete (int id)
        {
            ClubContext db = new ClubContext();
            News news = new News();
            db.News.Attach(news);
            news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("List");
        }

    }
}