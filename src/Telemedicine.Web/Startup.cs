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
            RouteConfig.Configure(RouteTable.Routes);
            WebApiConfig.Configure(GlobalConfiguration.Configuration);
            AuthConfig.Configure(app);

            app.MapSignalR();
          //  ExecuteMigrations();
        }

        private void ExecuteMigrations()
        {
            var configuration = new Telemedicine.Core.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}