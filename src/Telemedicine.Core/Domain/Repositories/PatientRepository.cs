using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class PatientRepository : EfRepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await Set
                .Include(t => t.User)
                .ToListAsync();
        }

        public Task<Patient> GetByUserIdAsync(string userId)
        {
            return Set.FirstOrDefaultAsync(t => t.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
        }

        public Patient GetByUserId(string userId)
        {
            return Set.FirstOrDefault(t => t.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)); 
        }

        public override Task<Patient> GetByIdAsync(int id)
        {
            return Set
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }        
    }
}