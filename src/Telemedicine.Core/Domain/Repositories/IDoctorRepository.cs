using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetByUserIdAsync(string userId);

        Doctor GetByUserId(string userId);

        IEnumerable<Doctor> GetAll();

        Task<IPagedList<Doctor>> PagedAsync(int page, int pageSize, string titleFilter, int specializationFilter);
    }
}
