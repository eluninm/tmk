using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Identity;

namespace Telemedicine.Core.Models
{
    public class ChatMessage : Entity
    {
        public DateTime Created { get; set; }

        public string Message { get; set; }

        public SiteUser Creator { get; set; }

        public string CreatorId { get; set; }

        public Conversation Conversation { get; set; }

        public string ConversationId { get; set; }
    }
}
