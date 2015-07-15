using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public Sex Sex { get; set; }
    }
}