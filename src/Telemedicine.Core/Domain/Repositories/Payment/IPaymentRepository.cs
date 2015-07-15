using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IPaymentRepository:IRepository<Payment>
    {
        Task<Payment> GetByPatientIdAsync(int id);
        Payment GetByPatientId(int id);
    }
}