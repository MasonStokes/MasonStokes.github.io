using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework8.Models;

namespace Homework8.Controllers
{
    public class BidsController : Controller
    {
        private Auction db = new Auction();

        // GET: Bids
        public ActionResult Index()
        {
            var bids = db.Bids.Include(b => b.Buyer).Include(b => b.Item);
            return View(bids.ToList());
        }

        // GET: Bids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // GET: Bids/Create
        public ActionResult Create()
        {
            ViewBag.BUYERID = new SelectList(db.Buyers, "ID", "NAME");
            ViewBag.ITEMID = new SelectList(db.Items, "ID", "NAME");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ITEMID,BUYERID,PRICE,TIMESTAMP")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Bids.Add(bid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BUYERID = new SelectList(db.Buyers, "ID", "NAME", bid.BUYERID);
            ViewBag.ITEMID = new SelectList(db.Items, "ID", "NAME", bid.ITEMID);
            return View(bid);
        }

        // GET: Bids/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bid bid = db.Bids.Find(id);
        //    if (bid == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BUYERID = new SelectList(db.Buyers, "ID", "NAME", bid.BUYERID);
        //    ViewBag.ITEMID = new SelectList(db.Items, "ID", "NAME", bid.ITEMID);
        //    return View(bid);
        //}

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,ITEMID,BUYERID,PRICE,TIMESTAMP")] Bid bid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bid).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BUYERID = new SelectList(db.Buyers, "ID", "NAME", bid.BUYERID);
        //    ViewBag.ITEMID = new SelectList(db.Items, "ID", "NAME", bid.ITEMID);
        //    return View(bid);
        //}

        // GET: Bids/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bid bid = db.Bids.Find(id);
        //    if (bid == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bid);
        //}

        // POST: Bids/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Bid bid = db.Bids.Find(id);
        //    db.Bids.Remove(bid);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult AllBids(int? id)
        {
            var bids = db.Bids.Where(b => b.ITEMID == id)
                              .Select(i => new { Buyer = i.Buyer.NAME, Amount = i.PRICE })
                              .OrderByDescending(b => b.Amount)
                              .ToList();

            return Json(bids, JsonRequestBehavior.AllowGet);

            //List<Bid> recentBids = db.Bids.OrderByDescending(x => x.TIMESTAMP).Take(10).ToList();

            //return View(recentBids);
        }
    }
}
