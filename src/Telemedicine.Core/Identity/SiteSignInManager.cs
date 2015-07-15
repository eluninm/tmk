using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Telemedicine.Core.Identity
{
    public class SiteSignInManager: SignInManager<SiteUser, string>
    {
        public SiteSignInManager(SiteUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {

        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(SiteUser user)
        {
            var userIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(ClaimTypes.Authentication, user.DisplayName));
            return userIdentity;
        }
    }
}
