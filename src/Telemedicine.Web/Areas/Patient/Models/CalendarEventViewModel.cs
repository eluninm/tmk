using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Patient.Models
{
    public class CalendarEventViewModel
    {
        [DisplayName("Дата события")]
        public DateTime Date { get; set; }

        [DisplayName("Тема")]
        public string Topic { get; set; }

        [DisplayName("Комментарий")]
        public string Comments { get; set; }
    }
}