using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class AddAppointmentEventViewModel
    {
        public string DoctorFIO { get; set; }

        public DateTime Date { get; set; }

        public string Topic { get; set; }
    }
}