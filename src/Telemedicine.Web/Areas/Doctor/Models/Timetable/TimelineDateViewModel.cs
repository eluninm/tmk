using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Telemedicine.Web.Areas.Doctor.Models.Timetable
{
    public class TimelineDateViewModel
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("hasConsultations")]
        public bool HasConsultations { get; set; }

        [JsonProperty("hours")] 
        public List<TimelineHourViewModel> Hours { get; set; }
    }
}
