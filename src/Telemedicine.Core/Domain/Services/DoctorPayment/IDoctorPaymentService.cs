using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public interface IDoctorPaymentService
    {
        Task<IEnumerable<DoctorPaymentHistory>> GetAllAsync();

        Task<DoctorPaymentHistory> CreateAsync(DoctorPaymentHistory doctorPaymentHistory);

        Task<DoctorPaymentHistory> GetByIdAsync(int id);

        Task<DoctorPaymentHistory> UpdateAsync(DoctorPaymentHistory doctorPaymentHistory);

        Task<IPagedList<DoctorPaymentHistory>> PagedAsync(int id, int page, int pageSize);
    }
}