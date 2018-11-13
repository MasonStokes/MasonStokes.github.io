# Homework 6

### Controller

```csharp
using Homework6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using Homework6.Models.ImportersViewModel;
using System.Net;

namespace Homework6.Controllers
{
    public class HomeController : Controller
    {
        private ImportersContext db = new ImportersContext();

        /// <summary>
        /// This action method searches the db for the correct people by FullName.
        /// </summary>
        /// <param name="search">A string, a full name is expected to query the db against.</param>
        /// <returns>A view that contains a list with people who match the name given search string.</returns>
        public ActionResult Index(String search)
        {
            ViewModel vmodel = new ViewModel();

            search = Request.QueryString["search"];
            /// Checking the the query string.
            if (search == null || search == "")
            {
                ViewBag.show = false;
                return View();
            }
            else
            {
                ViewBag.show = true;
                /// This takes the people from db who have a FullName that contains search.
                return View(db.People.Where(p => p.FullName.Contains(search)).ToList());
            }
        }

        /// <summary>
        /// This action method finds the information that is connected to a given person.
        /// </summary>
        /// <param name="id">An id value found from the persn who was searched.</param>
        /// <returns>A view with the personal information of the person who was searched for.</returns>
        public ActionResult Details(int? id)
        {
            ViewModel vmodel = new ViewModel();

            vmodel.Person = db.People.Find(id);

            if (vmodel.Person.Customers2.Count() <= 0)
            {
                ViewBag.SomeBody = false;
            }
            else
            {
                //lets show the information
                ViewBag.Somebody = true;

                int cid = vmodel.Person.Customers2.FirstOrDefault().CustomerID;

                vmodel.Customer = db.Customers.Find(cid);

                //find the gross sales
                ViewBag.GrossSales = vmodel.Customer.Orders.SelectMany(il => il.Invoices).SelectMany(ils => ils.InvoiceLines).Sum(i => i.ExtendedPrice);

                //find the gross profit
                //This will return a null value which creates make the view through a argumentnullexception.
                ViewBag.GrossProfits = vmodel.Customer.Orders.SelectMany(invoice => invoice.Invoices).SelectMany(invoicelines => invoicelines.InvoiceLines).Sum(profit => profit.LineProfit);

                //selects the information on the top ten sales
                vmodel.InvoiceLine = vmodel.Customer.Orders.SelectMany(x => x.Invoices).SelectMany(i => i.InvoiceLines).OrderByDescending(i => i.LineProfit).Take(10).ToList();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(vmodel);
        }
    }
}
```

### Search View

```html
@model IEnumerable<Homework6.Models.Person>

@{
    ViewBag.Title = "Homework 6";
}



<h2>Client Search</h2>

@using (Html.BeginForm("Index", "Home", FormMethod.Get, new { @class = "form-inline" }))
{
    <div class=" form-group">
        <div class="input-group" id=" searchbutton">
            @Html.TextArea("search", "", new { @class = "form-control", @placeholder = "Enter Name Here", required = "required"})
            <span class="input-group-btn" >
                <button id="button" class="btn btn-primary" type="submit">Search</button>
            </span>
        </div>
    </div>
}

@if (ViewBag.show)
{
    if(Model.Count() == 0)
    {
        <h3>Sorry couldn't find a match.</h3>
    }
    else
    {
        <div>
            <h3>Client Name:</h3>
        </div><br />
        foreach (var person in Model)
        {
            <div>
                <a class="btn btn-default btn-lg" href="Home/Details/@person.PersonID" role="button">@person.FullName (@person.PreferredName)</a>
            </div>
        }
    }
}
```

### Details View

```html
@model Homework6.Models.ImportersViewModel.ViewModel

@{
    ViewBag.Title = "Details";
}


<div>
    <div class="col-md-6">
        @*This the section of the view that contains personal information.*@
        <div class="row" id="description">
            <h3 id="Person-info">@Model.Person.FullName</h3>
            <dl class="dl-horizontal">
                <dt>@Html.Label("preferred-name", "Preferred Name:")</dt>
                <dd>@Html.DisplayFor(model => model.Person.PreferredName)</dd>

                <dt>@Html.Label("phone-number", "Phone Number:")</dt>
                <dd>@Html.DisplayFor(model => model.Person.PhoneNumber)</dd>

                <dt>@Html.Label("fax-number", "Fax Number:")</dt>
                <dd>@Html.DisplayFor(model => model.Person.FaxNumber)</dd>

                <dt>@Html.Label("email-address", "Email Address:")</dt>
                <dd>
                    <a href="mailto:@Html.DisplayFor(model => model.Person.EmailAddress)">@Html.DisplayFor(model => model.Person.EmailAddress)</a>
                </dd>

                <dt>@Html.Label("member-since", "Member Since:")</dt>
                <dd>@Html.DisplayFor(model => model.Person.ValidFrom)</dd>
            </dl>
        </div>
        <br />

        @*This the the section of the view that contains company information.*@
        <div class="row" id="description"></div>
        @if (ViewBag.SomeBody == true)
        {
            <h3 id="Company-info">Company Profile</h3>

            <dl class="dl-horizontal">
                <dt>@Html.Label("customer-name", "Customer Name:")</dt>
                <dd>@Html.DisplayFor(model => model.Customer.CustomerName)</dd>

                <dt>@Html.Label("phone-number", "Phone Number:")</dt>
                <dd>@Html.DisplayFor(model => model.Customer.PhoneNumber)</dd>
                
                <dt>@Html.Label("fax-number", "Fax Number:")</dt>
                <dd>@Html.DisplayFor(model => model.Customer.FaxNumber)</dd>

                <dt>@Html.Label("website-url", "Website URL:")</dt>
                <dd>
                    <a href="@Model.Customer.WebsiteURL">@Html.DisplayFor(model => model.Customer.WebsiteURL)</a>
                </dd>

                <dt>@Html.Label("member-since", "Member Since:")</dt>
                <dd>@Html.DisplayFor(model => model.Customer.ValidFrom)</dd>
            </dl>
            <br />

            @*This is the section of the view that contains the purchase information.*@
            <h3 class="row" id="Company-info">Purchase History Summary</h3>
            <dl class="dl-horizontal">

                <dt> @Html.Label("order-count", "Count:") </dt>
                <dd> @Html.DisplayFor(model => model.Customer.Orders.Count) </dd>

                <dt> @Html.Label("gross-sales", "Gross Sales:") </dt>
                <dd> @String.Format("{0:C}", ViewBag.GrossSales) </dd>

                <dt> @Html.Label("gross-profit", "Gross Profit:") </dt>
                @*When this isn't returning a value properly. I continue to get a argument null exception.*@
                <dd> @*@String.Format("{0:C}", ViewBag.GrossProfits)*@ </dd>

            </dl>
            <br />

            @*This is the section of the view that contains the top 10 purchase items.*@
            <div class="row" id="description">
                <h3 id="purchase-info">Items Purchased</h3>
                <table class="table">
                    <thead>
                        <tr>
                            <th scope="col">
                                @Html.Label("stock-item", "Stock Item ID")
                            </th>
                            <th scope="col">
                                @Html.Label("description", "Description")
                            </th>
                            <th scope="col">
                                @Html.Label("line-profit", "Line Profit")
                            </th>
                            <th scope="col">
                                @Html.Label("sales-person", "Sales Person")
                            </th>
                        </tr>
                    </thead>

                    @foreach (var items in Model.InvoiceLine)
                    {
                        <tbody>
                            <tr>
                                <td>@Html.DisplayFor(item => items.StockItemID)</td>

                                <td>@Html.DisplayFor(item => items.Description)</td>

                                <td>@String.Format("{0:C}", items.LineProfit)</td>

                                <td>@Html.DisplayFor(item => items.Invoice.Person4.FullName)</td>
                            </tr>
                        </tbody>
                    }
                </table>
            </div>
        }
    </div>

    <div class="col-md-6">
        <div class="row">
            <a href="https://placeholder.com"><img src="https://via.placeholder.com/150" /></a>
        </div>
        <br />

        @*
        <div class="row" id="description">
            <div id="map" style="width:650px; height:300px;"> </div>
            <script>
                var longitude = @Model.Customer.DeliveryLocation.Longitude;
                var latitude = @Model.Customer.DeliveryLocation.Latitude;

                var map = L.map('map').setView([latitude, longitude], 13);

                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                }).addTo(map);

                L.marker([lat, long]).addTo(map)
                    .bindPopup('<p>' + @Model.Customer.City.CityName + '</p>').openPopup();
            </script>
        </div>
        *@

    </div>
</div>
<br />

<div class="row">
    <p class="col-lg-12">
        @Html.ActionLink("Back to Search", "Index")
    </p>
</div>
```