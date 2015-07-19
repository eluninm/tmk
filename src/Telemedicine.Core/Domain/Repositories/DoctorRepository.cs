using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

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

        public async Task<IPagedList<Doctor>> PagedAsync(int page, int pageSize, string titleFilter, int specializationFilter)
        {
            var query = Set
                .Include(t => t.User)
                .Include(t => t.Specialization)
                .Include(p => p.DoctorStatus);

            if (!string.IsNullOrWhiteSpace(titleFilter))
            {
                var searchString = titleFilter.Trim();
                var groups = searchString.Split(' ');
                if (groups.Length > 0)
                {
                    var ss = groups[0];
                    query = query.Where(t => t.User.LastName.ToUpper().Contains(ss.ToUpper()));
                }
                if (groups.Length > 1)
                {
                    var ss = groups[1];
                    query = query.Where(t => t.User.FirstName.ToUpper().Contains(ss.ToUpper()));
                }
                if (groups.Length > 2)
                {
                    var ss = groups[2];
                    query = query.Where(t => t.User.MiddleName.ToUpper().Contains(ss.ToUpper()));
                }
                //else
                //{
                //    query = query.Where(t =>
                //        t.User.FirstName.ToUpper().Contains(titleFilter.ToUpper())
                //        || t.User.LastName.ToUpper().Contains(titleFilter.ToUpper())
                //        || t.User.MiddleName.ToUpper().Contains(titleFilter.ToUpper()));
                //}
            }
            if (specializationFilter != 0)
            {
                query = query.Where(t => t.SpecializationId == specializationFilter);
            }

            return await query.OrderBy(t => t.Id).ToPagedListAsync(page, pageSize);
        }

        public override Doctor GetById(int id)
        {
            return Set.Include(p => p.Specialization).FirstOrDefault(item => item.Id == id);
        }
    }
}