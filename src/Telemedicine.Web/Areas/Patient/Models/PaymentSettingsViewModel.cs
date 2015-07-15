using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class PaymentSettingsViewModel
    {
        [DisplayName("Метод оплаты")]
        public int SelectedMethodId { get; set; } 
        
        public IEnumerable<SelectListItem> PaymentMethodListItems { get; set; }

        [DisplayName("Тариф")]
        public int SelectedTariffId { get; set; }


        [DisplayName("Пополнить баланс на")]
        public Double Balance { get; set; }

        [DisplayName("Автоплатеж")]
        public bool AutoPayment { get; set; }

        public IEnumerable<SelectListItem> TariffListItems { get; set; } 
        public IEnumerable<PaymentHistoryViewModel> PaymentHistoryViewModel { get; set; } 
    }
}