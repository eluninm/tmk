using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Models.Enums; 

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class AddEventViewModel
    {
        [DisplayName("Дата начала")]
        public DateTime Begin { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime? End { get; set; }

        [DisplayName("Весь день")]
        public bool IsOnDate { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Повторять событие")]
        public bool IsRepeat { get; set; }


        [DisplayName("Понедельник")]
        public bool Monday { get; set; }
        [DisplayName("Вторник")]
        public bool Tuesday { get; set; }
        [DisplayName("Среда")]
        public bool Wednesday { get; set; }
        [DisplayName("Четверг")]
        public bool Thursday { get; set; }
        [DisplayName("Пятница")]
        public bool Friday { get; set; }
        [DisplayName("Суббота")]
        public bool Saturday { get; set; }
        [DisplayName("Воскресенье")]
        public bool Sunday { get; set; } 

        [DisplayName("Повторяется")]
        public TypeRepeatingEvent SelectedRepeatType { get; set; } 

        [DisplayName("Повторять с интервалом")]
        public int SelectedIntervalId { get; set; }

        public IEnumerable<SelectListItem> IntervalItems { get; set; } 

        public EndTypeRepeatInterval SelectedEndType { get; set; } 

        public int After { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime? RepeatEndDate { get; set; }

        [DisplayName("Дата начала")]
        public DateTime RepeatBeginDate { get; set; }
    }
}