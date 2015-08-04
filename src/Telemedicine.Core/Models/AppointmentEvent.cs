using System;
using System.ComponentModel.DataAnnotations.Schema;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Models.Enums;

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
         
        public AppointmentStatus Status { get; set; }

        public int? ConsultationId { get; set; }

        public DoctorTimetable DoctorTimetable { get; set; }

        public int DoctorTimetableId { get; set; }
    }
}