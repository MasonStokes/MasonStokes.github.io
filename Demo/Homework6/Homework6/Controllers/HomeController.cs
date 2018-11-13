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