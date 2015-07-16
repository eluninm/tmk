using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace Telemedicine.Web.Api.Dto
{
    public class RecommendationDto
    {
        public int RecommendationId { get; set; }

        public DateTime Created { get; set; }

        public string DoctorTitle { get; set; }

        public string DoctorSpecialization { get; set; }

        public string RecommendationText { get; set; }
    }
}