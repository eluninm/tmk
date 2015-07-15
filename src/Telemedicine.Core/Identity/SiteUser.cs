using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Identity
{
    /// <summary>
    /// Site user represents any user logged in.
    /// </summary>
    public class SiteUser : IdentityUser
    {
        public SiteUser()
        {
            UserEvents = new List<UserEvent>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [NotMapped]
        public string DisplayName => $"{LastName} {FirstName} {MiddleName}";

        public DateTime? LastLoginDate { get; set; }

        public string PaymentInstrument { get; set; }
        public virtual List<UserEvent> UserEvents { get; set; }
    }
}
