using ClubManagement.DAL;
using ClubManagement.Models;
using ClubManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClubManagement.Controllers
{
    public class RoleController : Controller
    {
       

        // GET: Role
        public ActionResult Index()
        {
            ClubContext db = new ClubContext();
            var roleList = db.Roles.ToList();

            return View(roleList);
        }
        public ActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            ClubContext db = new ClubContext();
            if (!ModelState.IsValid) return View("Create", role);

            else
            {

                Role r = new Role
                {
                    RoleName = role.RoleName

                };

                db.Roles.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }

        [HttpGet]
        public ActionResult RoleToUser()
        {
            ClubContext db = new ClubContext();
            
            List<Account> accList = db.Accounts.ToList();
            List<Role> roleList = db.Roles.ToList();

            var viewModel = new RoleToUserVM()
            {
                Accounts = accList,
                Roles = roleList,
            };
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult RoleToUser(RoleToUserVM roleToUserVM)
        {
            ClubContext db = new ClubContext();
            Account acc = db.Accounts.Find(Convert.ToInt32(roleToUserVM.Name));
            Role role = db.Roles.Find(Convert.ToInt32(roleToUserVM.Role));
            
            acc.Role = role.RoleName;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}