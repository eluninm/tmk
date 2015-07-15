using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class UserEventViewModel
    {
        [DisplayName("Дата события")]
        public DateTime Date { get; set; }

        [DisplayName("Тема")]
        public string Topic { get; set; }

        [DisplayName("Комментарий")]
        public string Comments { get; set; }
    }
}