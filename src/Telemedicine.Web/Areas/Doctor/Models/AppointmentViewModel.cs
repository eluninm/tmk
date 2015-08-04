using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string PatientFio { get; set; }

        public DateTime Date { get; set; }
    }
}