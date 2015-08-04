using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public interface IDoctorTimetableService
    {
        Task<IEnumerable<DoctorTimetable>> GetDoctorTimetableByMonthAsync(int doctorId, int year, int month);

        Task<IEnumerable<DoctorTimetable>> GetAllAsync();

        Task<DoctorTimetable> CreateAsync(DoctorTimetable timetable);

        Task<DoctorTimetable> GetByIdAsync(int id);

        Task<DoctorTimetable> UpdateAsync(DoctorTimetable timetable);

        Task Delete(DoctorTimetable timetable);

        Task<DoctorTimetable> GetTimetableByDate(int doctorId, DateTime dateTime);
    }
}