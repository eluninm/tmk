using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface ITimeSpanEventRepository:IRepository<TimeSpanEvent>
    {
        Task<IEnumerable<TimeSpanEvent>> GetTimeSpansByDates(int doctorId, DateTime startSearchDate,
            DateTime stopSearchDate);
    }
}