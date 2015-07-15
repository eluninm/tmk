using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class PatientCreateViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [DisplayName("Возраст")]
        public int Age { get; set; }

        [DisplayName("Пол")]
        public Sex Sex { get; set; }
    }
}