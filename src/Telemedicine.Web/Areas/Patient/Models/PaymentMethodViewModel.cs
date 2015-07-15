using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class PaymentMethodViewModel
    {
        public string CurrentStatusDisplayName { get; set; }

        public IEnumerable<PaymentMethodItemViewModel> AvailableMethods { get; set; }

    }
}
     