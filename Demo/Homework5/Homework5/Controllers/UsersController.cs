using Homework5.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework5.Models;


namespace Homework5.Controllers
{
    public class UsersController : Controller
    {
        private UserContext db = new UserContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult User()
        {

            return View();
        }
    }
}