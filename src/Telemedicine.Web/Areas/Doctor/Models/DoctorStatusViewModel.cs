using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Domain.Models;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class DoctorStatusViewModel
    {
        public DoctorStatus CurrentStatus { get; set; }

        public IEnumerable<DoctorStatus> AvailableStatuses { get; set; }
    }
}