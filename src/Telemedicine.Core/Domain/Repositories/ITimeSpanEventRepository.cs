using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface ITimeSpanEventRepository:IRepository<TimeSpanEvent>
    {
        /*Task<TimeSpanEvent> GetByCalendarEventIdAsync(string userId);

        Doctor GetByCalendarEventId(string userId);*/
    }
}