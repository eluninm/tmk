using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetByUserIdAsync(string userId);

        Doctor GetByUserId(string userId);
        
    }
}
