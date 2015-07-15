using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Identity;

namespace Telemedicine.Core.Models
{
    public class Conversation : Entity<string>
    {
        public DateTime Created { get; set; }

        public SiteUser Creator { get; set; }

        public ICollection<SiteUser> Members { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }
    }
}
