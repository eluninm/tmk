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
    public class DoctorRepository : EfRepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public override async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await Set
                .Include(t => t.User)
                .Include(t => t.Specialization).Include(p=>p.DoctorStatus)
                .ToListAsync();
        }

        public override Task<Doctor> GetByIdAsync(int id)
        {
            return Set
                .Include(t => t.User)
                .Include(t => t.Specialization).Include(p=>p.DoctorStatus)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<Doctor> GetByUserIdAsync(string userId)
        {
            return Set.Include(t=>t.User).Include(t=>t.Specialization).FirstOrDefaultAsync(t => t.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
        }

        public Doctor GetByUserId(string userId)
        {
            return Set.FirstOrDefault(t => t.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Doctor> GetAll()
        {
            return  Set
                .Include(t => t.User)
                .Include(t => t.Specialization).Include(p => p.DoctorStatus)
                .ToList();
        }

        public override Doctor GetById(int id)
        {
            return Set.Include(p => p.Specialization).FirstOrDefault(item => item.Id == id);
        }
    }
}