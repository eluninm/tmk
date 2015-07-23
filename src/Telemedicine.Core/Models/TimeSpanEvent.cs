using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Models
{
    public class TimeSpanEvent : Entity
    { 
        
        public string Title { get; set; }
         
        public DateTime BeginDate { get; set; }
         
        public DateTime? EndDate { get; set; }
         
        public virtual SiteUser Owner { get; set; }

        public TypeRepeatingEvent RepeatType { get; set; }
         
        public EndTypeRepeatInterval Interval { get; set; }
        public int RepeatCount { get; set; }
        public int RepeatInterval { get; set; }

        public bool IsRepeat { get; set; }
         
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        [NotMapped]
        public int DoctorId { get; set; }
    }
}
