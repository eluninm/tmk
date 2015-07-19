using System.Data.Entity.Migrations;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Telemedicine.Web.Startup))]
namespace Telemedicine.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseContainer();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Configure);
            RouteConfig.Configure(RouteTable.Routes);
            AuthConfig.Configure(app);
            app.MapSignalR();
        }
    }
}