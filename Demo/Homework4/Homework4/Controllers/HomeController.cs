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

        [HttpGet]
        public ActionResult Mile()
        {
            double miles = Convert.ToDouble(Request.QueryString["miles"]);
            string metric = Request.QueryString["metric"];
            double output = 0;
            Debug.WriteLine(miles);
            Debug.WriteLine(metric);
            ViewBag.result = false;

            if(metric == "mm")
            {
                output = miles * 1609344;
                ViewBag.result = true;
            }
            else if (metric == "cm")
            {
                output = miles * 160934.4;
                ViewBag.result = true;
            }
            else if (metric == "m")
            {
                output = miles * 1609.344;
                ViewBag.result = true;
            }
            else if (metric == "km")
            {
                output = miles * 1.609344;
                ViewBag.result = true;
            }

            string message = miles + "miles is equal to " + output + " " + metric;

            ViewBag.Message = message;

            return View();
        }

        public ActionResult Miles()
        {
            ///What to use for the color changer html page.
            //@using (Html.BeginForm("Miles","Home",FormMethod.Post))
            //{
                //@Html.Label("Name","Name")
                //@Html.TextBox("Name", null,htmlAttributes:new { @class="form-control", type="number"})
            //}
            return View();//@Html.Label("Name","Name")
        }
        
    }
}