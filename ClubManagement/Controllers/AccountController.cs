using ClubManagement.DAL;
using ClubManagement.Models;
using ClubManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClubManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult List()
        {
            ClubContext mDb = new ClubContext();
            List<Account> accounts = mDb.Accounts.ToList();
            return View(accounts);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            ClubContext baza = new ClubContext();
            
            Account a = new Account() { Id = id };
            baza.Accounts.Attach(a);
            baza.Accounts.Remove(a);
            baza.SaveChanges();
            return RedirectToAction("Lista");
        }
        public ActionResult Register()
        {

            return View();
        }
        public JsonResult CheckUsernameAvailability(string userdata)
        {
            ClubContext db = new ClubContext();
            System.Threading.Thread.Sleep(200);
            var SeachData = db.Accounts.Where(x => x.Nick == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        [HttpPost]
        public ActionResult Register(RegisterVM reg)
        {
            Account a = new Account();
            ClubContext mDb = new ClubContext();


            if (!ModelState.IsValid) return View("Register", reg);

            else
            {
                Account acc = new Account
                {
                    Nick = reg.Nick,
                    Password = reg.Password,
                    Role = reg.Role,


                };
                
                mDb.Accounts.Add(acc);
                mDb.SaveChanges();

                return RedirectToAction("MainPage", "Home");
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account acc, string ReturnUrl = "")
        {
            ClubContext baza = new ClubContext();
            if (ModelState.IsValid)
            {
                var user = baza.Accounts.Where(a => a.Nick.Equals(acc.Nick) && a.Password.Equals(acc.Password)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(acc.Nick, false);
                    Session["nick"] = acc.Nick;
                    Session["rola"] = user.Role;

                    if(ReturnUrl=="") return RedirectToAction("MainPage", "Home");
                    else 
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Nie poprawna nazwa użytkownika lub hasło");
                    ViewBag.Message = "Niepoprawny login lub hasło";
                    return View("Login");
                }
            }
            else return View("Login");

        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["nick"] = null;
            return RedirectToAction("MainPage", "Home");
        }
    }
}