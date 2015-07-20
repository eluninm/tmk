using System;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class AppointmentEvent:Entity
    {
        public DateTime Created { get; set; }

        public DateTime Date { get; set; }

        public Patient Patient { get; set; }

        public int PatientId { get; set; }

        public Doctor Doctor { get; set; }

        public int DoctorId { get; set; }
    }
}