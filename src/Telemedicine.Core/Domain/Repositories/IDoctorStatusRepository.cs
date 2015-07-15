using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IDoctorStatusRepository : IRepository<DoctorStatus>
    {
        Task<IEnumerable<DoctorStatus>> AvailableStatusesAsync();

        Task<DoctorStatus> GetByNameAsync(string statusName);

        DoctorStatus GetByName(string statusName);
        IEnumerable<DoctorStatus> GetAvailableStatuses();
    }
}
