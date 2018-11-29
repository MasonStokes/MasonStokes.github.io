using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Printing;

namespace Homework_4.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Taking the input from the view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ColorChooser()
        {
            ViewBag.show = false;
            return View();
        }

        /// <summary>
        /// Taking the input from the view and combines the two colors together.
        /// </summary>
        /// <returns>ColorChooser view</returns>
        [HttpPost]
        public ActionResult ColorChooser(String c1, String c2)
        {

            c1 = Request.Form["FirstColor"];
            c2 = Request.Form["SecondColor"];

            Debug.WriteLine(c1);
            Debug.WriteLine(c2);

            Color rgb1 = ColorTranslator.FromHtml(c1);
            Color rgb2 = ColorTranslator.FromHtml(c2);

            int a = 0;
            int r = 0;
            int g = 0;
            int b = 0;

            if (rgb1.A + rgb2.A >= 255)
            {
                a = 1;
            }
            else
            {
                a = rgb1.A + rgb2.A;
            }
            if (rgb1.R + rgb2.R >= 255)
            {
                r = 255;
            }
            else
            {
                r = rgb1.R + rgb2.R;
            }
            if (rgb1.G + rgb2.G >= 255)
            {
                g = 255;
            }
            else
            {
                g = rgb1.G + rgb2.G;
            }
            if (rgb1.B + rgb2.B >= 255)
            {
                b = 255;
            }
            else
            {
                b = rgb1.B + rgb2.B;
            }
            

            String result = ColorTranslator.ToHtml(Color.FromArgb(a, r, g, b));

            if(c1 != null && c2 != null)
            {
                ViewBag.show = true;

                ViewBag.color1 = "width:90px; height:90px; border: solid #000; background:" + c1 + "; ";
                ViewBag.color2 = "width:90px; height:90px; border: solid #000; background:" + c2 + "; ";
                ViewBag.result = "width:90px; height:90px; border: solid #000; background:" + result + "; ";
            }

            return View();
        }
    }
}