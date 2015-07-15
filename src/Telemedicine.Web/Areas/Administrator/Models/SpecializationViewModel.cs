using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class SpecializationViewModel
    {
        public int Id { get; set; }

        [DisplayName("Код")]
        public int Code { get; set; }

        [DisplayName("Название")]
        public string DisplayName { get; set; }
    }
}