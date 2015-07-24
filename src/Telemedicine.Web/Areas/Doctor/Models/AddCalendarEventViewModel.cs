using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class AddCalendarEventViewModel
    {
        public DateTime Date { get; set; }

        public int Duration { get; set; }
    }
}