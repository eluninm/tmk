using System;
using Newtonsoft.Json;
using Telemedicine.Core.Models;

namespace Telemedicine.Web.Areas.Doctor.Models.Timetable
{
    public class TimelineHourViewModel
    {
        //[JsonProperty("hour")]
        public int Hour { get; set; }

        //[JsonProperty("hourType")]
        public DoctorTimetableHourType HourType { get; set; }

        //[JsonProperty("patientsCount")]
        public int PatientsCount { get; set; }

        public string DateTime { get; set; }
    }
}