using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class ConsultationInfoDto
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public string DoctorTitle { get; set; }

        public string DoctorSpecialization { get; set; }
    }
}