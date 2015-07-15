using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetByUserIdAsync(string userId);
         Patient  GetByUserId(string userId);
    }
}
