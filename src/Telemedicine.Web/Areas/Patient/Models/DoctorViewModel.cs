using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public string Specialization { get; set; }

        public string Status { get; set; }

        public string SiteUserId { get; set; }

        public string StatusName { get; set; }

        public bool CanStartChat { get; set; }
    }
}