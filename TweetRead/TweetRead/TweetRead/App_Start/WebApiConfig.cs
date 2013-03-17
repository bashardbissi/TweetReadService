using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TweetRead
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}",
              defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "GetAndSaveUserTweets",
                routeTemplate: "api/{controller}/{ScreenName}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "GetTweetsByKey",
                routeTemplate: "api/{controller}/{KeyName}/{KeyValue}/",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
