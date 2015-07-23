using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Telemedicine.Web.Api.Formatters;

namespace Telemedicine.Web
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
           // config.SuppressDefaultHostAuthentication();
           // config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var jsonFormatter = config.Formatters.JsonFormatter;
            var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            jsonSettings.Converters.Add(new Datetimepicker3DateTimeConvertor());
            jsonFormatter.SerializerSettings = jsonSettings;
        }
    }
}