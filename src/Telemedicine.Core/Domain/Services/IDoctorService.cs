using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public interface IDoctorService
    {
        DoctorStatus GetStatusById(int doctorId);

        DoctorStatus GetStatusByUserId(string userId);

        DoctorStatus SetStatusByDoctorId(int doctorId, int statusId);

        DoctorStatus SetStatusByUserId(string userId, int statusId);

        Task<IEnumerable<DoctorStatus>> GetAvailableStatusesAsync();

        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<IPagedList<Doctor>> PagedAsync(int page = 1, int pageSize = 10, string titleFilter = null, int specializationFilter= 0);

        IEnumerable<Doctor> GetAll();

        Task<IEnumerable<Specialization>>  GetSpecializationsAsync();

        Task<Doctor> CreateAsync(Doctor doctor);

        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor> GetByUserIdAsync(string id);

        Task<Doctor> UpdateAsync(Doctor doctor);

        IEnumerable<DoctorStatus> GetAvailableStatuses();

        Doctor GetById(int id);

        Task Debit(int doctorId, double amount);
    }
}
