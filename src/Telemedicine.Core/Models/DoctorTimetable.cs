using System;
using System.Collections.Generic;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class DoctorTimetable : Entity
    {
        public DateTime DateTime { get; set; }

        public Doctor Doctor { get; set; }

        public int DoctorId { get; set; }

        public DoctorTimetableHourType HourType { get; set; }

        public virtual ICollection<AppointmentEvent> AppointmentEvents { get; set; }
    }

    public enum DoctorTimetableHourType
    {
        Working,
        NotWorking
    }
}
