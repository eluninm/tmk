using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Telemedicine.Core.Models.Enums
{
    public enum PaymentType
    {
        [Display(Name = "Пополнение")]
        Replenishment,
        [Display(Name = "Консультация")]
        Consultation
    }
}