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