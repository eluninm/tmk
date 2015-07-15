using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class CalendarEventModelView
    {
        public DateTime Date { get; set; } 
        public string Topic { get; set; }

        public string Comments { get; set; }
    }
}