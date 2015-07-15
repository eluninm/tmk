using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class TariffViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название тарифа")]
        public string Name { get; set; }

        [DisplayName("Стоймость")]
        public double Cost { get; set; }

        [DisplayName("Количество сообщений")]
        public int Messages { get; set; }

        [DisplayName("Количество минут")]
        public int Minutes { get; set; }
    }
}