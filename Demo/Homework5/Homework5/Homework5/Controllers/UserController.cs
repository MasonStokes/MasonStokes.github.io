using Homework5.DAL;
using Homework5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Homework5.Controllers
{
    public class UserController : Controller
    {
        private UserContext db = new UserContext();

        // GET: User
        public ActionResult Index()
        {
            var list = db.Users.ToList();
            var orderedList = list.OrderBy(item => item.SubmitTime);
            return View(orderedList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNumber,ApartmentName,UnitNumber,Explanation,Permission,SubmitTime")] User user)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}