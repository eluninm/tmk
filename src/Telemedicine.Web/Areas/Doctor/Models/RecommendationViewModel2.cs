using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class RecommendationViewModel2
    {
        public DateTime Date { get; set; }

        public string DoctorFIO { get; set; }

        public string DoctorSpecialization { get; set; }

        public string Content { get; set; }
    }
}