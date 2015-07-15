using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class PaymentHistoryViewModel
    {
        public int PatientId { get; set; }
        public PaymentType PaymentType { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string ConversationId { get; set; }
    }
}