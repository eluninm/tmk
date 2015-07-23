using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class TimeSpanEventRepository: EfRepositoryBase<TimeSpanEvent>, ITimeSpanEventRepository
    {
        public TimeSpanEventRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<TimeSpanEvent>> GetTimeSpansByDates(int doctorId, DateTime startSearchDate, DateTime stopSearchDate)
        {
            return await Set.Where(t => t.DoctorId == doctorId && t.BeginDate >= startSearchDate && t.BeginDate <= stopSearchDate).ToListAsync();
        }
    }
}
