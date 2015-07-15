using System.ComponentModel.DataAnnotations;

namespace Telemedicine.Core.Models.Enums
{
    public enum Sex
    {
        [Display(Name = "Мужчина")]
        Male,
        [Display(Name = "Женщина")]
        Female
    }
}