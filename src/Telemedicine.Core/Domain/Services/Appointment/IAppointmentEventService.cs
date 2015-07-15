using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IAppointmentEventService
    {
        Task<IEnumerable<AppointmentEvent>> GetAllAsync();

        Task<AppointmentEvent> CreateAsync(AppointmentEvent appointmentEvent);

        Task<AppointmentEvent> GetByIdAsync(int id);

        Task<AppointmentEvent> UpdateAsync(AppointmentEvent appointmentEvent); 
    }
}