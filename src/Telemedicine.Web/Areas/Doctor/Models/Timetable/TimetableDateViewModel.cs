using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telemedicine.Core.Models;

namespace Telemedicine.Web.Areas.Doctor.Models.Timetable
{
    public class TimetableDateViewModel
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("hasConsultations")]
        public bool HasConsultations { get; set; }

        [JsonProperty("hours")] 
        public List<TimetableHourViewModel> Hours { get; set; }
    }

    public class TimetableHourViewModel
    {
        [JsonProperty("hour")]
        public int Hour { get; set; }

        [JsonProperty("hourType")]
        public DoctorTimetableHourType HourType { get; set; }

        [JsonProperty("patientsCount")]
        public int PatientsCount { get; set; }
    }
}
