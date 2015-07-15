using System.Collections.Generic;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IPaymentMethodRepository:IRepository<PaymentMethod>
    {
        IEnumerable<PaymentMethod> GetAll();
    }
}