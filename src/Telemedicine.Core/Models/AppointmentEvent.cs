using System;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class AppointmentEvent:Entity
    {
        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateCreation { get; set; }
    }
}