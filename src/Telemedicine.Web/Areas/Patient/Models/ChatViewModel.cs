using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class ChatDialogViewModel
    {
        public string DoctorDisplayName { get; set; }

        public string DoctorUserId { get; set; }

        public string Specialization { get; set; }
    }
}