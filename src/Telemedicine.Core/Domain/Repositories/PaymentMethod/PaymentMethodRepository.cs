using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class PaymentMethodRepository :EfRepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}