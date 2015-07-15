using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<Payment> CreateAsync(Payment payment);

        Task<Payment> GetByIdAsync(int id);

        Task<Payment> UpdateAsync(Payment payment);

        Task<Payment> GetByPatientIdAsync(int id);

        Payment GetByPatientId(int id);
    }
}