using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework8.DAL;

namespace Homework8.Controllers
{
    public class HomeController : Controller
    {
        private AuctionContext db = new AuctionContext();
        public ActionResult Index()
        {
            return View(db.Bids.OrderByDescending(x => x.TIMESTAMP).Take(10).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}