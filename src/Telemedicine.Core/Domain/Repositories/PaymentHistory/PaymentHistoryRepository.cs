using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class PaymentHistoryRepository :EfRepositoryBase<PaymentHistory>, IPaymentHistoryRepository
    {
        public PaymentHistoryRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
        public IEnumerable<PaymentHistory> GetByPatientIdAsync(int id)
        {
            return  Set.Where(item => item.Patient.Id == id);
        }
    }
}