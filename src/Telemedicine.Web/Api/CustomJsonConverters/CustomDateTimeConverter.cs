using Newtonsoft.Json.Converters;

namespace Telemedicine.Web.Api.CustomJsonConverters
{
    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}