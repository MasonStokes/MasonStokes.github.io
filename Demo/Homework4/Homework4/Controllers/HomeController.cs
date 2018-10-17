using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Converter()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ColorChooser()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Mile()
        {
            Debug.WriteLine(Request.QueryString["user_name"]);
            Debug.WriteLine(Request.QueryString["user_email"]);
            Debug.WriteLine(Request.QueryString["user_message"]);

            string name = Request.QueryString["user_name"];
            if(name != null)
            {
                string message = "Hello" + name + "! Welcome.";
                ViewBag.message = message;
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Mile2()
        {
            return View();
        }
    }
}