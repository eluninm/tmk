using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> GetAllAsync();

        Task<PaymentMethod> CreateAsync(PaymentMethod paymentMethod);

        Task<PaymentMethod> GetByIdAsync(int id);

        Task<PaymentMethod> UpdateAsync(PaymentMethod paymentMethod);
         
    }
}