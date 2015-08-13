using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Api.CustomJsonConverters;

namespace Telemedicine.Web.Api.Dto
{
    public class DoctorAppointmentDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string PatientTitle { get; set; }

        public string PatientAvatarUrl { get; set; } 

        public AppointmentStatus Status { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Date { get; set; }
    }
}