using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class SelectPatientViewModel
    {
        public int SelectedPatientId { get; set; }

        [DisplayName("ФИО пациента")]
        public IEnumerable<SelectListItem> Patients { get; set; } 
    }
}