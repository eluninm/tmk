using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicine.Core.Domain.Models;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class SetStatusViewModel
    {
        public DoctorStatus Status { get; set; }
    }
}