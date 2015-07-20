using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Telemedicine.Core.Identity
{
    public class SiteUserManager : UserManager<SiteUser>
    {
        public SiteUserManager(IUserStore<SiteUser> store)
            : base(store)
        {
        }

        public override Task<IdentityResult> UpdateAsync(SiteUser user)
        {
            if (string.IsNullOrEmpty(user.AvatarUrl))
            {
                user.AvatarUrl = "/img/no_avatar.png";
            }
            return base.UpdateAsync(user);
        }
    }
}
