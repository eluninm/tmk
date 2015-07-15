using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Telemedicine.Core.Data;

namespace Telemedicine.Core.Identity
{
    public class SiteUserStore : UserStore<SiteUser>
    {
        public SiteUserStore(IDbContextProvider dbContextProvider) : base(dbContextProvider.Context)
        {
            
        }
    }
}
