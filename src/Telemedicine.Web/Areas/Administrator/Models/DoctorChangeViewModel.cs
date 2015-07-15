using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class DoctorChangeViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Специализация")]
        public int SpecializationId { get; set; }
    }
}