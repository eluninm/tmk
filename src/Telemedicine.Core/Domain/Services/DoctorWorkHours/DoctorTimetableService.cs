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
    }
}