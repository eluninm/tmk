using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class RecommendationViewModel
    { 
        [DisplayName(@"Краткое описание")]
        public string RecommendationText { get; set; }  

        public string PatientUserId { get; set; }
    }
}