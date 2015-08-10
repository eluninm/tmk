using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Api.CustomJsonConverters;

namespace Telemedicine.Web.Api.Dto
{
    public class PaymentHistoryDto
    {
        [JsonConverter(typeof (CustomDateTimeConverter))]
        public DateTime Date { get; set; }

        public string PaymentType { get; set; }
        public double Value { get; set; }
        public string PatientTitle { get; set; }
    }
}