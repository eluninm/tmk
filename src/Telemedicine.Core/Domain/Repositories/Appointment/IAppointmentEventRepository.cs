﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IAppointmentEventRepository : IRepository<AppointmentEvent>
    {
        Task<IPagedList<AppointmentEvent>> GetDoctorAppointmentsPagedAsync(int doctorId, int page, int pageSize, string patientTitleFilter, DateTime? start, DateTime? end, bool? needDeclined, bool? needReady, bool? needClosed, string filter);

        Task<IEnumerable<AppointmentEvent>> GetDoctorAppointmentsByDateAsync(int doctorId, DateTime date);

        Task<IPagedList<AppointmentEvent>> GetPatientAppointmentsPagedAsync(int patientId, int page, int pageSize);
    }
}
