using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class PatientAppointmentDto
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DoctorTitle { get; set; }

        public string DoctorSpecialization { get; set; }

        public DateTime Date { get; set; }
    }
}