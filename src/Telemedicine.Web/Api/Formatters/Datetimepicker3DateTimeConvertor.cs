using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telemedicine.Web.Api.Formatters
{
    public class Datetimepicker3DateTimeConvertor : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd hh:mm"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }
    }
}