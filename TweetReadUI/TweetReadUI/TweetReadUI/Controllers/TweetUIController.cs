using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;
using DataContracts;
using System.IO;
using System.Text;

namespace TweetReadUI.Controllers
{
    public class TweetUIController : Controller
    {
        //
        // GET: /TweetUI/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAndSaveUserTweets(string ScreenName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49303/");

            // Add an Accept header for JSON format. 
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            string response = client.GetStringAsync("api/Tweet/" + ScreenName).Result;

            ViewData["GetAndSaveUserTweetsReturnValue"] = response;

            return View("Index");

           // return Content(response);
        }

        public ActionResult GetTweetsCountBySource(string Source)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49303/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string response = client.GetStringAsync("api/Tweet/TweetSource/" + Source).Result;

            System.Runtime.Serialization.Json.DataContractJsonSerializer _JsonSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<Tweet>));
           
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(response));
            List<Tweet> _lsttweets =  (List<Tweet>)_JsonSerializer.ReadObject(ms);

            ViewData["GetTweetsCountBySourceReturnValue"] = "Your search returned (" + _lsttweets.Count + ") tweets posted via " + Source ;

            return View("Index");

            ;
        }

        public ActionResult GetTweetsCountByTimezone(string TweetTimezone)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49303/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string response = client.GetStringAsync("api/Tweet/TweetTimezone/" + TweetTimezone).Result;

            System.Runtime.Serialization.Json.DataContractJsonSerializer _JsonSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<Tweet>));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(response));
            List<Tweet> _lsttweets = (List<Tweet>)_JsonSerializer.ReadObject(ms);

            ViewData["GetTweetsCountByTimezoneReturnValue"] = "Your search returned (" + _lsttweets.Count + ") tweets posted from " + TweetTimezone;

            return View("Index");

            ;
        }

       

    }
}



