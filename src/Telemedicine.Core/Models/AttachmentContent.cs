

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class AttachmentContent : Entity
    {
        public byte[] Content { get; set; }
    }
}
