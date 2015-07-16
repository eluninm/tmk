using System.Web.Http;

namespace Telemedicine.Web
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Web Api attribute routes.
            config.MapHttpAttributeRoutes();
        }
    }
}