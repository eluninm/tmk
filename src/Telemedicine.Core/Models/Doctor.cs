using Newtonsoft.Json;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Identity;

namespace Telemedicine.Core.Models
{
    /// <summary>
    /// Represents Doctor object in system.
    /// </summary>
    public class Doctor : Entity
    {
        public SiteUser User { get; set; }

        public string UserId { get; set; }

        public DoctorStatus DoctorStatus { get; set; }

        public int DoctorStatusId { get; set; }

        public Specialization Specialization { get; set; }

        public int SpecializationId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
