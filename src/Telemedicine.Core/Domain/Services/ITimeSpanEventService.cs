using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface ITimeSpanEventService
    {
        Task<IEnumerable<TimeSpanEvent>> GetAllAsync(); 

        Task<TimeSpanEvent> CreateAsync(TimeSpanEvent timeSpanEvent);

        Task<TimeSpanEvent> GetByIdAsync(int id);

        Task<TimeSpanEvent> UpdateAsync(TimeSpanEvent timeSpanEvent);

        void Delete(TimeSpanEvent timeSpanEvent);
    }
}
