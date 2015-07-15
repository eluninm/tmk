using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class MedicalHistoryViewModel
    {
        public DateTime Date { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }

        public string TargetId { get; set; }
    }
}