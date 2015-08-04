using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories.DoctorWorkHours;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class DoctorTimetableService : IDoctorTimetableService
    {
        private readonly IDoctorTimetableRepository _doctorWorkHourrsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorTimetableService(IDoctorTimetableRepository doctorWorkHourrsRepository, IUnitOfWork unitOfWork)
        {
            _doctorWorkHourrsRepository = doctorWorkHourrsRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<DoctorTimetable>> GetDoctorTimetableByMonthAsync(int doctorId, int year, int month)
        {
            return _doctorWorkHourrsRepository.GetDoctorTimetableByMonthAsync(doctorId, year, month);
        }

        public Task<IEnumerable<DoctorTimetable>> GetAllAsync()
        {
            return _doctorWorkHourrsRepository.GetAllAsync();
        }

        public async Task<DoctorTimetable> CreateAsync(DoctorTimetable timetable)
        {
            DoctorTimetable newEntity = _doctorWorkHourrsRepository.Insert(timetable);
            await _unitOfWork.SaveChangesAsync();
            return newEntity;
        }

        public Task<DoctorTimetable> GetByIdAsync(int id)
        {
            return _doctorWorkHourrsRepository.GetByIdAsync(id);
        }

        public async Task<DoctorTimetable> UpdateAsync(DoctorTimetable timetable)
        {
            DoctorTimetable newEntity = _doctorWorkHourrsRepository.Update(timetable);
             await _unitOfWork.SaveChangesAsync();
            return newEntity;
        }

        public async Task Delete(DoctorTimetable timetable)
        {
            _doctorWorkHourrsRepository.Delete(timetable);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<DoctorTimetable> GetTimetableByDate(int doctorId, DateTime dateTime)
        {
            return _doctorWorkHourrsRepository.GetTimetableByDate(doctorId, dateTime);
        }
    }
}