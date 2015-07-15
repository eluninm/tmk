using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Identity;

namespace Telemedicine.Core.Models
{
    public class UserEvent : Entity
    {         
        public SiteUser Creator { get; set; }        
        
        public string Content { get; set; }

        public DateTime DateCreation { get; set; }
    }
}
