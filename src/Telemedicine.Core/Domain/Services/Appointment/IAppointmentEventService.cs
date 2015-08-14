using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public interface IAppointmentEventService
    {
        Task<IEnumerable<AppointmentEvent>> GetAllAsync();

        Task<AppointmentEvent> CreateAsync(AppointmentEvent appointmentEvent);

        Task<AppointmentEvent> GetByIdAsync(int id);

        Task<AppointmentEvent> UpdateAsync(AppointmentEvent appointmentEvent);

        Task<IPagedList<AppointmentEvent>> GetDoctorAppointmentsPagedAsync(int doctorId, int page = 1, int pageSize = 10, string patientTitleFilter = null,
            DateTime? start = null, DateTime? end = null, bool? needDeclined = null, bool? needReady = null, bool? needClosed = null, string filter = null);

        Task<IEnumerable<AppointmentEvent>> GetDoctorAppointmentsByDateAsync(int doctorId, DateTime date);

        Task<IPagedList<AppointmentEvent>> GetPatientAppointmentsPagedAsync(int id, int page = 1, int pageSize = 10);
    }
}