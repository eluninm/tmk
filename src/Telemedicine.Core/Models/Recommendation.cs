using System;
using System.Collections.Generic;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class Recommendation : Entity
    {
        public DateTime CreateDate { get; set; }

        public string RecommendationText { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public ICollection<Attachment> Attachments { get; set; } 

        public bool IsMarkedAsDeleted { get; set; }
    }
}
