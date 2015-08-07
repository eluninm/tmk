using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Api.Dto
{
    public class PaymentHistoryDto
    {
       public  DateTime Date { get; set; }
       public string PaymentType { get; set; }
       public double Value { get; set; }
       public string PatientTitle { get; set; }
    }
}