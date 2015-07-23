using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class PatientHistoryPageDto
    {
        public IEnumerable<PaymentHistoryDto> PaymentItems { get; set; }

        public int TotalCount { get; set; }
    }
}