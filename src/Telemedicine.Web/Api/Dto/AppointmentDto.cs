using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Telemedicine.Web.Api.CustomJsonConverters;

namespace Telemedicine.Web.Api.Dto
{
    public class AppointmentDto
    {
        public int DoctorId { get; set; }
         
        public DateTime AppointmentDate { get; set; }
    }
}