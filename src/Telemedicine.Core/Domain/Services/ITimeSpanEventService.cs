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

        Task<DoctorTimeWindows> GetDoctorTimeWindowsAsync(int id);
    }

    public class DoctorTimeWindows
    {
        public int WindowSize { get; set; }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

        public DateTime NearestAvailable { get; set; }

        public IEnumerable<DateTime> EnabledDates { get; set; }

        public IEnumerable<DateTime> DisabledDates { get; set; }
    }
}
