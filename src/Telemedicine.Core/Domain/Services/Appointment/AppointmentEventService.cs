using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public class AppointmentEventService : IAppointmentEventService
    {
        private readonly IAppointmentEventRepository _appointmentEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentEventService(IAppointmentEventRepository appointmentEventRepository, IUnitOfWork unitOfWork)
        {
            _appointmentEventRepository = appointmentEventRepository;
            _unitOfWork = unitOfWork;
        }

        public  Task<IEnumerable<AppointmentEvent>> GetAllAsync()
        {
            return  _appointmentEventRepository.GetAllAsync();
        }

        public async Task<AppointmentEvent> CreateAsync(AppointmentEvent appointmentEvent)
        {
            _appointmentEventRepository.Insert(appointmentEvent);
            await _unitOfWork.SaveChangesAsync();
            return appointmentEvent;
        }

        public Task<AppointmentEvent> GetByIdAsync(int id)
        {
            return _appointmentEventRepository.GetByIdAsync(id);
        }

        public async Task<AppointmentEvent> UpdateAsync(AppointmentEvent appointmentEvent)
        {
            var newAppointment = _appointmentEventRepository.Update(appointmentEvent);
            await _unitOfWork.SaveChangesAsync();
            return newAppointment;
        }

        public Task<IPagedList<AppointmentEvent>> GetDoctorAppointmentsPagedAsync(int doctorId, int page = 1, int pageSize = 10, string patientTitleFilter= null, DateTime? start = null, DateTime? end = null, AppointmentStatus? status = null)
        {
            return _appointmentEventRepository.GetDoctorAppointmentsPagedAsync(doctorId, page, pageSize, patientTitleFilter, start, end,status);
        }

        public Task<IEnumerable<AppointmentEvent>> GetDoctorAppointmentsByDateAsync(int doctorId, DateTime date)
        {
            return _appointmentEventRepository.GetDoctorAppointmentsByDateAsync(doctorId, date);
        }

        public Task<IPagedList<AppointmentEvent>> GetPatientAppointmentsPagedAsync(int patientId, int page = 1, int pageSize = 10)
        {
            return _appointmentEventRepository.GetPatientAppointmentsPagedAsync(patientId, page, pageSize);
        }
    }
}