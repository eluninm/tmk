using System;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Repositories
{
    public class DoctorPaymentRepository :EfRepositoryBase<DoctorPaymentHistory>, IDoctorPaymentRepository
    {
        public DoctorPaymentRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IPagedList<DoctorPaymentHistory>> PagedAsync(int id, int page, int pageSize, DateTime? start, DateTime? end)
        {
            var query = Set.Where(item => item.DoctorId == id);
            if (start.HasValue  && end.HasValue)
            {
                query = query.Where(item => item.Date >= start && item.Date < end);
            }
            return await query.OrderBy(t => t.Id).ToPagedListAsync(page, pageSize);
        }
    }
}