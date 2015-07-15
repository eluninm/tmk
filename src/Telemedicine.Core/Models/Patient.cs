using System.Runtime.InteropServices;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Models
{
    public class Patient : Entity
    {
        public SiteUser User { get; set; }

        public string UserId { get; set; }

        public int Age { get; set; }

        public Sex Sex { get; set; }

        public double Balance { get; set; }
    }
}
