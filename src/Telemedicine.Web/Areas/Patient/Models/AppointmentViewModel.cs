using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DisplayName("Специализация")]
        public string Specialization { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата")]
        [Required]
        public DateTime Date { get; set; }
    }
}