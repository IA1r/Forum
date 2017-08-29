using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //     name: "NoContrl",
            //     url: "{action}/{id}/{*catchall}",
            //     defaults: new { controller = "Topic", action = "Topics", id = UrlParameter.Optional }
            // );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{*catchall}",
                defaults: new { controller = "Topic", action = "Sections", id = UrlParameter.Optional }
            );


        }
    }
}