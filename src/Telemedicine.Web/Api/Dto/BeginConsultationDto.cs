using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class BeginConsultationDto
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }



    }
}