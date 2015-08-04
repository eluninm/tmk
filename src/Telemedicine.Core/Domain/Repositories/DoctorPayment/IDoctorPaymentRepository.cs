using System;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IDoctorPaymentRepository:IRepository<DoctorPaymentHistory>
    {
        Task<IPagedList< DoctorPaymentHistory>> PagedAsync(int id, int page, int pageSize, DateTime? start, DateTime? end);
    }
}