using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Domain.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class DoctorStatusRepository : EfRepositoryBase<DoctorStatus>, IDoctorStatusRepository
    {
        public DoctorStatusRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<IEnumerable<DoctorStatus>> AvailableStatusesAsync()
        {
            return GetAllAsync();
        }

        public Task<DoctorStatus> GetByNameAsync(string statusName)
        {
            return Set.FirstOrDefaultAsync(t => t.Name.Equals(statusName, StringComparison.OrdinalIgnoreCase));
        }

        public DoctorStatus GetByName(string statusName)
        {
            return Set.FirstOrDefault(t => t.Name.Equals(statusName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<DoctorStatus> GetAvailableStatuses()
        {
            return Set.ToList();
        }
    }
}