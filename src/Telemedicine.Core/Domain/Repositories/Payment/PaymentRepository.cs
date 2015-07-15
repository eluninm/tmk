using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class PaymentRepository : EfRepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Payment> GetByPatientIdAsync(int id)
        {
            return await Set.FirstOrDefaultAsync(item => item.Patient.Id == id);
        }
    }
}