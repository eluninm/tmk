using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Models
{
    public class PaymentHistory:Entity
    {
        public Patient Patient { get; set; }
        public PaymentType PaymentType { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string ConversationId { get; set; }
    }
}
