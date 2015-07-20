using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class AppointmentDto
    {
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}