using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IPaymentHistoryRepository: IRepository<PaymentHistory>
    {
        IEnumerable<PaymentHistory> GetByPatientIdAsync(int id);
    }
}