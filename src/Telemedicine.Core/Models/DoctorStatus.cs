using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Domain.Models
{
    public class DoctorStatus : Entity
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}
