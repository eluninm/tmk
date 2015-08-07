using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Models
{
    public class DoctorPaymentHistory:Entity
    {
        public int DoctorId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public PaymentHistory PatientPayment { get; set; }
    }
}
