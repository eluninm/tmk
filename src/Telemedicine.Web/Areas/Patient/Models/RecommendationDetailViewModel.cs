using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class RecommendationDetailViewModel
    {
        public DateTime Date { get; set; }
        public string DoctorFIO { get; set; }
        public string SpecializationDisplayName { get; set; }
        public string Content { get; set; }
    }
}