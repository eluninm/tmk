using System.Web.Mvc;
using System.Web.Routing;

namespace Telemedicine.Web
{
    public static class RouteConfig
    {
        public static void Configure(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Redirect", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Telemedicine.Web.Controllers" }
            );
        }
    }
}