# Homework 8



### ER Diagram

!(/images/ERDiagram.png)


### SQL up and down scripts to build out the database.

```sql

CREATE TABLE [dbo].[Buyers]
(
	[ID]			INT IDENTITY (1,1)							NOT NULL,
	[NAME]			NVARCHAR(100)								NOT NULL

	CONSTRAINT [PK_dbo.Buyers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Sellers]
(
	[ID]			INT IDENTITY (1,1)							NOT NULL,
	[NAME]			NVARCHAR(100)								NOT NULL

	CONSTRAINT [PK_dbo.Sellers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Items]
(
	[ID]			INT IDENTITY (1001,1)						NOT NULL,
	[NAME]			NVARCHAR(100)								NOT NULL,
	[DESCRIPTION]	NVARCHAR(MAX)								NOT NULL,
	[SellerID]		INT FOREIGN KEY REFERENCES Sellers(ID)		NOT NULL

	CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED ([ID] ASC),
);

CREATE TABLE [dbo].[Bids]
(
	[ID]			INT IDENTITY (1, 1)							NOT NULL,
	[ITEMID]		INT	FOREIGN KEY REFERENCES Items(ID)		NOT NULL,
	[BUYERID]		INT	FOREIGN KEY REFERENCES Buyers(ID)		NOT NULL,
	[PRICE]			INT											NOT NULL,
	[TIMESTAMP]		DATETIME									NOT NULL

	CONSTRAINT [PK_dbo.Bids] PRIMARY KEY CLUSTERED ([ID] ASC)
	
);

INSERT INTO [dbo].[Buyers] (NAME) VALUES
	('Jane Stone'),
	('Tom McMasters'),
	('Otto Vanderwall')

INSERT INTO [dbo].[Sellers](NAME) VALUES
	('Gayle Hardy'),
	('Lyle Banks'),
	('Pearl Greene')

INSERT INTO [dbo].[Items](NAME, DESCRIPTION, SellerID) VALUES
	('Abraham Lincoln Hammer','A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 3),
	('Albert Einsteins Telescope','A brass telescope owned by Albert Einstein in Germany, circa 1927', 1),
	('Bob Dylan Love Poems','Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 2)

INSERT INTO [dbo].[Bids](ITEMID, BUYERID, PRICE, TIMESTAMP) VALUES
	(1001, 3, 250000,'12/04/2017 09:04:22'),
	(1003, 1, 95000 ,'12/04/2017 08:44:03')

GO



--Take the User table down
DROP TABLE [dbo].[Bids]
DROP TABLE [dbo].[Items]
DROP TABLE [dbo].[Sellers]
DROP TABLE [dbo].[Buyers]

```




### Item Create View

```html
@model Homework8.Models.Item

@{
    ViewBag.Title = "Create";
}

<h2>Create</h2>


@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
<div class="form-horizontal">
    <h4>Item</h4>
    <hr />
    @Html.ValidationSummary(true, "", new { @class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(model => model.NAME, htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.NAME, new { htmlAttributes = new { @class = "form-control" } })
            @Html.ValidationMessageFor(model => model.NAME, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.DESCRIPTION, htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.DESCRIPTION, new { htmlAttributes = new { @class = "form-control" } })
            @Html.ValidationMessageFor(model => model.DESCRIPTION, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.SellerID, "Seller Name", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.DropDownList("SellerID", null, htmlAttributes: new { @class = "form-control" })
            @Html.ValidationMessageFor(model => model.SellerID, "", new { @class = "text-danger" })
        </div>
    </div>

    @*<div class="form-group">
            @Html.LabelFor(model => model.SellerID, "SellerID", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("SellerID", null, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.SellerID, "", new { @class = "text-danger" })
            </div>
        </div>*@

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Create" class="btn btn-default" />
        </div>
    </div>
</div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}


```




### Item Delete View

```html
@model Homework8.Models.Item

@{
    ViewBag.Title = "Delete";
}

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Item</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.Seller.NAME)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Seller.NAME)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.NAME)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.NAME)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.DESCRIPTION)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.DESCRIPTION)
        </dd>

    </dl>

    @using (Html.BeginForm()) {
        @Html.AntiForgeryToken()

        <div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    }
</div>


```




### Item Details View

```html
@model Homework8.Models.Item

@{
    ViewBag.Title = "Details";
}

<h2>Details</h2>

<div>
    <h4>Item</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            Seller Name
        </dt>

        <dd>
            <input type="hidden" id="sellerName" value="@Model.ID" />
            @Html.DisplayFor(model => model.Seller.NAME)
        </dd>

        <dt>
            Item Name
        </dt>

        <dd>
            @Html.DisplayFor(model => model.NAME)
        </dd>

        <dt>
            Description
        </dt>

        <dd>
            @Html.DisplayFor(model => model.DESCRIPTION)
        </dd>

    </dl>
</div>

<p>
    @Html.ActionLink("Place a bid", "Create", "Bids") |
    @Html.ActionLink("Edit", "Edit", new { id = Model.ID }) |
    @Html.ActionLink("Back to List", "Index")
</p>

@*Bids table (where bids will show up via JavaScript)*@
<table class="table">
    <tr>
        <th>
            Bidder
        </th>
        <th>
            Bid amount
        </th>
    </tr>
</table>

@*Displays message if there are no bids on this item*@
<h5 id="message" style="text-align:center"></h5>
<br />



@section BidScript
{
    <script type="text/javascript" src="~/Scripts/Bids.js"></script>    
}

```

### Item Edit View

```html
@model Homework8.Models.Item

@{
    ViewBag.Title = "Edit";
}

<h2>Edit</h2>


@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Item</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        @Html.HiddenFor(model => model.ID)

        <div class="form-group">
            @Html.LabelFor(model => model.NAME, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.NAME, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.NAME, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.DESCRIPTION, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.DESCRIPTION, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.DESCRIPTION, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.SellerID, "SellerID", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("SellerID", null, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.SellerID, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}


```




### Item Index View

```html
@model IEnumerable<Homework8.Models.Item>

@{
    ViewBag.Title = "Index";
}

<h2>Items</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            Seller Name
        </th>
        <th>
            Name of Item
        </th>
        <th>
            Item Description
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Seller.NAME)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.NAME)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.DESCRIPTION)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.ID }) |
            @Html.ActionLink("Details", "Details", new { id=item.ID }) |
            @Html.ActionLink("Delete", "Delete", new { id=item.ID })
        </td>
    </tr>
}

</table>



```




### Item Controller

```csharp
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
    public class ItemsController : Controller
    {
        private Auction db = new Auction();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Seller);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.SellerID = new SelectList(db.Sellers, "ID", "NAME");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,DESCRIPTION,SellerID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SellerID = new SelectList(db.Sellers, "ID", "NAME", item.SellerID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerID = new SelectList(db.Sellers, "ID", "NAME", item.SellerID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,DESCRIPTION,SellerID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellerID = new SelectList(db.Sellers, "ID", "NAME", item.SellerID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}



```




### Bid Create View

```html
@model Homework8.Models.Bid

@{
    ViewBag.Title = "Create";
}

<h2>Create Bid</h2>


@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
<div class="form-horizontal">
    <h4>Bid</h4>
    <hr />
    @Html.ValidationSummary(true, "", new { @class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(model => model.ITEMID, "Item Name", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.DropDownList("ITEMID", null, htmlAttributes: new { @class = "form-control" })
            @Html.ValidationMessageFor(model => model.ITEMID, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.BUYERID, "Buyer Name", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.DropDownList("BUYERID", null, htmlAttributes: new { @class = "form-control" })
            @Html.ValidationMessageFor(model => model.BUYERID, "", new { @class = "text-danger" })
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(model => model.PRICE, "Bid amount", htmlAttributes: new { @class = "control-label col-md-2" })
        <div class="col-md-10">
            @Html.EditorFor(model => model.PRICE, new { htmlAttributes = new { @class = "form-control" } })
            @Html.ValidationMessageFor(model => model.PRICE, "", new { @class = "text-danger" })
        </div>
    </div>

    @*<div class="form-group">
            @Html.LabelFor(model => model.TIMESTAMP, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.TIMESTAMP, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.TIMESTAMP, "", new { @class = "text-danger" })
            </div>
        </div>*@

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Create" class="btn btn-default" />
        </div>
    </div>
</div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}


```




### Bid Details View

```html
@model Homework8.Models.Bid

@{
    ViewBag.Title = "Details";
}

<h2>Bid Details</h2>

<div>
    <h4>Bid</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.Buyer.NAME)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Buyer.NAME)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.Item.NAME)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Item.NAME)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.PRICE)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.PRICE)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.TIMESTAMP)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.TIMESTAMP)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", new { id = Model.ID }) |
    @Html.ActionLink("Back to List", "Index")
</p>


```




### Bid Index View

```html
@model IEnumerable<Homework8.Models.Bid>

@{
    ViewBag.Title = "Index";
}

<h2>Bids</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.Buyer.NAME)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Item.NAME)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.PRICE)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.TIMESTAMP)
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Buyer.NAME)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Item.NAME)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.PRICE)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.TIMESTAMP)
        </td>
        <td>
            @*@Html.ActionLink("Edit", "Edit", new { id=item.ID }) |*@
            @Html.ActionLink("Details", "Details", new { id=item.ID }) |
            @*@Html.ActionLink("Delete", "Delete", new { id=item.ID })*@
        </td>
    </tr>
}

</table>


```




### Bid controller

```csharp
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


```




### bid javascript

```javascript


function bidRequest() {
    var input = document.getElementById("sellerName").value;
    var id = parseInt(input);
    var source = "/Bids/AllBids/" + id; //appends it to URI string.

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: itemBids,
        error: ajaxError
    });
}


function itemBids(bidList) {
    if (bidList.length == 0) {
        $("#message").empty();
        $("#message").append("No bids to display. Be the first to bid on this item.");
    }
    else {
        $("#message").empty();
        $(".bid-info").remove();
        for (var i = 0; i < bidList.length; i++) {
            $("table").append("<tr class=\"bid-info\"><td>" + bidList[i].Buyer + "</td><td>$" + Number(bidList[i].Amount).toLocaleString('en-US', { minimumFractionDigits: 2 }) + "</td></tr>");
        }
    }
}


function ajaxError() {
    alert("Error with ajax");
}


function main() {
    bidRequest(); 

    var interval = 1000 * 5;

    window.setInterval(bidRequest, interval);
}

$(document).ready(main());

//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "ajax": {
//            "url": "/Home/GetData",
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            { "data": "Name" },
//            { "data": "Position" },
//            { "data": "Office" },
//            { "data": "Age" },
//            { "data": "Salary" }
//        ]
//    });
//}); 


```


