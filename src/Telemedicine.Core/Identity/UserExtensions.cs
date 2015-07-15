using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Telemedicine.Core.Identity
{
    public static class UserExtensions
    {
        /// <summary>
        /// Return the user display name using the UserNameClaimType
        /// </summary>
        /// <param name="identity"/>
        /// <returns/>
        public static string GetUserDisplayName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity?.FindFirstValue(ClaimTypes.Authentication);
        }
    }
}
