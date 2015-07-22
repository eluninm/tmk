using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class DoctorAppointmentDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string PatientTitle { get; set; }

        public string PatientAvatarUrl { get; set; }

        public DateTime Date { get; set; }
    }
}