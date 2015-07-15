using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class PaymentViewModel
    {
        [DisplayName("Метод оплаты")]
        public int SelectedMethodId { get; set; }

        public IEnumerable<SelectListItem> PaymentMethodListItems { get; set; }
    }
}