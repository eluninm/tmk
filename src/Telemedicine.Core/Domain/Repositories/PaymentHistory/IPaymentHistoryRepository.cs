using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IPaymentHistoryRepository: IRepository<PaymentHistory>
    {
        IEnumerable<PaymentHistory> GetByPatientIdAsync(int id);

        Task<IPagedList<PaymentHistory>> PagedAsync(string id, int page, int pageSize);
    }
}