using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public interface IPaymentHistoryService
    {
        Task<IEnumerable<PaymentHistory>> GetAllAsync();

        Task<PaymentHistory> CreateAsync(PaymentHistory paymentHistory);

        Task<PaymentHistory> GetByIdAsync(int id);

        Task<PaymentHistory> UpdateAsync(PaymentHistory paymentHistory);

        IEnumerable<PaymentHistory> GetByPatientIdAsync(int id);

        Task<IPagedList<PaymentHistory>> PagedAsync(string id, int page, int pageSize);
    }
}