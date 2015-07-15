using System.ComponentModel;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class PaymentMethodViewModel
    { 
        public int Id { get; set; }
        [DisplayName("Способ оплаты")]
        public string Name { get; set; } 
    }
}