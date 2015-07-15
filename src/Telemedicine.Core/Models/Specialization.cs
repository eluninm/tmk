using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class Specialization : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public int Code { get; set; }

        public int? ParentId { get; set; }
        
        public Specialization Parent { get; set; }
    }
}
