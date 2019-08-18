using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeveloperBlogAPI {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "GetAll", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ModelRoute",
                url: "{controller}/{action}/{model}",
                defaults: new { controller = "Post", action = "Save", model = UrlParameter.Optional }
            );
        }
    }
}
