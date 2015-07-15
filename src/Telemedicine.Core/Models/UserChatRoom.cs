using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class UserChatRoom : Entity
    {
        public string UserId { get; set; }

        public string RoomId { get; set; }
    }
}
