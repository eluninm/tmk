using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Areas.Administrator.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string  MiddleName { get; set; }

        public string Specialization { get; set; }

        public string Email { get; set; }
    }
}