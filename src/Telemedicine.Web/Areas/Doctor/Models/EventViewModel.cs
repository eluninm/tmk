using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            allDay = false; 
        }
         
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public string color { get; set; }
        public bool allDay { get; set; } 
    }
}