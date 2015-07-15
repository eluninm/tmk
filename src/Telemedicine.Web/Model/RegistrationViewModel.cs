using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Model
{
    public class RegistrationViewModel
    {
        [Required]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Отчество")]
        public string SurName { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }
    }
}