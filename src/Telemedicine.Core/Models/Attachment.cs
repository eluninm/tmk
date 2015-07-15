using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class Attachment : Entity
    {
        public string Description { get; set; }

        public string FileName { get; set; }

        public AttachmentContent Content { get; set; }
    }
}
