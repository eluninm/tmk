using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class UserEventRepository : EfRepositoryBase<UserEvent>, IUserEventRepository
    {
        public UserEventRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
