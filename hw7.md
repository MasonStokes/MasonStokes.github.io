# Homework 7

This web page takes an input of text and returns a gif for given words.

### Giphy Controller

```csharp
using Homework7.DAL;
using Homework7.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Homework7.Controllers
{
    public class GiphyController : Controller
    {
        private LogContext SearchLog = new LogContext();

        public ActionResult Gif()
        {
            return View();
        }

        [HttpGet]
        public JsonResult RandomGif(string lastWord)
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["APIKEY"];
            Debug.WriteLine("apikey : " + apiKey);

            string giphyUrl = "http://api.giphy.com/v1/stickers/translate?api_key=" + apiKey + "&s=" + lastWord;
            Debug.WriteLine("giphy url : " + giphyUrl);

            WebRequest sendRequest = WebRequest.Create(giphyUrl);

            WebResponse getResponse = sendRequest.GetResponse();
            Debug.WriteLine("get response : " + getResponse);

            Stream data = getResponse.GetResponseStream();

            StreamReader reader = new StreamReader(data);
            string convertResponse = reader.ReadToEnd();
            Debug.WriteLine("convert response : " + convertResponse);

            var serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonResponse = serialize.DeserializeObject(convertResponse);

            SearchLog searchLogDb = SearchLog.SearchLogs.Create();

            searchLogDb.TIMESTAMP = DateTime.Now;
            searchLogDb.REQUESTTYPE = Request.Url.OriginalString;
            searchLogDb.CLIENTIP = Request.UserHostAddress;
            searchLogDb.BROWSER = Request.Browser.Type;

            SearchLog.SearchLogs.Add(searchLogDb);
            SearchLog.SaveChanges();

            reader.Close();
            data.Close();
            getResponse.Close();

            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }
    }
}

```



### Gif View

```html
@{
    ViewBag.Title = "Gif";
}

<h2>Gif</h2>

<div class="row inputArea">
    <div style="width: 100%; margin: 0 auto;">
            <!-- Create the input bar -->
            @Html.TextBox("inputString", "", new{ @class = "form-control", id = "searchInput", placeholder = "Begin typing your message here", type = "text" })

            <!-- Add a button to refresh the page and let the client start over -->
            <br/>
            <input type="Reset" value="Reset" onClick="window.location.reload()">
    </div>
</div>


<div id="container" style="margin-left: 20px;">
    <p>
        <div id="message"></div>
    </p>
</div>

@section scriptSection
{
    <script src="~/Scripts/giphy.js" type="text/javascript"></script>
}

```



### Up and Down Sql for the logger

```sql
CREATE TABLE [dbo].[SearchLogs]
(
	[ID]			INT IDENTITY (1, 1)	NOT NULL,
	[TIMESTAMP]		DATETIME			NOT NULL,
	[REQUESTTYPE]	NVARCHAR (100)		NOT NULL,
	[CLIENTIP]		NVARCHAR (100)		NOT NULL,
	[BROWSER]		NVARCHAR (100)		NOT NULL

	CONSTRAINT [PK_dbo.SearchLogs] PRIMARY KEY CLUSTERED ([ID] ASC)
);


DROP TABLE [dbo].[SearchLogs]

```

### Log model

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework7.Models
{
    public class SearchLog
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Holds the time of the request.
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Holds the type of the request.
        /// </summary>
        public string REQUESTTYPE { get; set; }

        /// <summary>
        /// Holds the client IP address.
        /// </summary>
        public string CLIENTIP { get; set; }

        /// <summary>
        /// Holds the clients browser information.
        /// </summary>
        public string BROWSER { get; set; }

        /// <summary>
        /// Overwrites ToString to print table entries.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()}: {TIMESTAMP} {REQUESTTYPE} {CLIENTIP} {BROWSER}";
        }

    }
}

```

### Log Context

```csharp
using Homework7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework7.DAL
{
    public class LogContext : DbContext
    {
        public LogContext() : base("name=SearchLogs") { }

        public virtual DbSet<SearchLog> SearchLogs { get; set; }
    }
}

```