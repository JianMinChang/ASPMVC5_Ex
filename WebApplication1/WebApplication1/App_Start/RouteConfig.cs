using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "ShoppingDeleteRoute",
                url: "Shopping/Delete/{ProductID}/{Quantity}/{Price}",
                defaults: new { controller = "Shopping", action = "Delete", ProductID = UrlParameter.Optional, Quantity = UrlParameter.Optional, Price = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ShoppingEditRoute",
                url: "Shopping/Edit/{ProductID}/{Quantity}/{Price}",
                defaults: new { controller = "Shopping", action = "Edit", ProductID = UrlParameter.Optional, Quantity = UrlParameter.Optional, Price = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductRoute",
                url: "Product/Index/{CategoryID}",
                defaults: new { controller = "Product", action = "Index", CategoryID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
