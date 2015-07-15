using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Models
{
    public class Payment:Entity
    {
        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public Tariff Tariff { get; set; } 
        public bool AutoPayment { get; set; } 
    }
}
