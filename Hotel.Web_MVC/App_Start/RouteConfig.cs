﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Hotel.Web_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Pay order",
                url: "room/{orderId}/pay",
                defaults: new { controller = "Order", action = "Pay" }
            );

            routes.MapRoute(
                name: "Cancel order",
                url: "room/{orderId}/cancel",
                defaults: new { controller = "Order", action = "Cancel" }
            );

            routes.MapRoute(
                name: "Order room",
                url: "room/{roomId}/book",
                defaults: new { controller = "Order", action = "Book" }
            );

            routes.MapRoute(
                name: "List of orders",
                url: "orders/",
                defaults: new { controller = "Order", action = "List" }
            );

            routes.MapRoute(
                name: "Search for free rooms",
                url: "rooms/",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
