# Homework 4

### Home Controller

This controller handles the logic for the metric converter page.

```csharp
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
            if (metric == "mm")
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
            string message = miles + " miles is equal to " + output + " " + metric;
            ViewBag.Message = message;
            return View();
        }

    }
}

```

### Color Controller

This controller handles the logic for the color changer page.

```csharp
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

```

### Home View

This view handles both that main landing page and the UI of the metric converter page, this is the code of the metric converter.

```html
@{
    ViewBag.Title = "Mile";
}
<h2>Convert Miles to Metric</h2>
@if (ViewBag.message != null)
{
    <h3>@ViewBag.message</h3>
}
<div class="row">
    <form action="/Home/Mile" method="get">
        <div class="col-md-6">
            <label for="miles">Miles:</label>
            <input type="text" id="name" name="miles" step=".001" required>
        </div>
        <div class="col-md-6">
            <h3>Select units:</h3>
            <input type="radio" id="mm" name="metric" value="mm" checked>Millimeter<br />
            <input type="radio" id="cm" name="metric" value="cm">Centimeter<br />
            <input type="radio" id="m" name="metric" value="m">Meter<br />
            <input type="radio" id="km" name="metric" value="km">Kilometer<br />
        </div>
        <center><button type="submit" class="btn btn-primary btn-lg">Convert</button></center>
        @if (ViewBag.statement != null)
        {
            <hw2>@ViewBag.statement</hw2>
        }
    </form>
</div>

```

### Color Chooser View

This view handles the UI for the color chooser.

```html
@{
    ViewBag.Title = "ColorChooser";
}

<h2>Color Chooser</h2>

<p>Enter color in Html hexidecimal form: #ABC123</p>

<div class="col-md-6">
    @using (Html.BeginForm("ColorChooser", "Color", FormMethod.Post))
    {
        <div class="for-group">
            @Html.Label("FirstColor", "First Color:")
            @Html.TextBox("FirstColor", "", new { @class = "form-control", @pattern = "#[0-9A-Fa-f]{3,6}", type = "text", @palceholder = "#ABC123", required = "required" })

            @Html.Label("SecondColor", "Second Color:")
            @Html.TextBox("SecondColor", "", new { @class = "form-control", @pattern = "#[0-9A-Fa-f]{3,6}", type = "text", @palceholder = "#ABC123", required = "required" })

        </div>

        <button type="submit" value="Submit" class="btn btn-primary">Submit</button>
    }
    
    <div class="row">
        <div class="col-sm-3" style="@ViewBag.color1"></div>
        <div class="col-sm-3" style="width:50px;" height: 100px;><h3>+</h3></div>
        <div class="col-sm-3" style="@ViewBag.color2"></div>
        <div class="col-sm-3" style="width:50px;" height: 100px;><h3>=</h3></div>
        <div class="col-sm-3" style="@ViewBag.result"></div>
    </div>
</div>

```