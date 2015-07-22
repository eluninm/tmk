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
    public class AppointmentEventRepository : EfRepositoryBase<AppointmentEvent>, IAppointmentEventRepository
    {
        public AppointmentEventRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public async override Task<IEnumerable<AppointmentEvent>> GetAllAsync()
        {
            return await Set.Include(t => t.Patient).Include(p=>p.Patient.User)
                .Include(t => t.Doctor).Include(t=>t.Doctor).Include(t => t.Doctor.User)
                .ToListAsync();
        }

        public async Task<IPagedList<AppointmentEvent>> GetDoctorAppointmentsPagedAsync(int doctorId, int page, int pageSize, string patientTitleFilter)
        {
            var query = Set
                .Include(t => t.Patient)
                .Include(t => t.Patient.User);

            if (!string.IsNullOrWhiteSpace(patientTitleFilter))
            {
                var searchString = patientTitleFilter.Trim();
                var groups = searchString.Split(' ');
                if (groups.Length > 0)
                {
                    var ss = groups[0];
                    query = query.Where(t => t.Patient.User.LastName.ToUpper().Contains(ss.ToUpper()));
                }
                if (groups.Length > 1)
                {
                    var ss = groups[1];
                    query = query.Where(t => t.Patient.User.FirstName.ToUpper().Contains(ss.ToUpper()));
                }
                if (groups.Length > 2)
                {
                    var ss = groups[2];
                    query = query.Where(t => t.Patient.User.MiddleName.ToUpper().Contains(ss.ToUpper()));
                }
            }

            return await query.OrderBy(t => t.Id).ToPagedListAsync(page, pageSize);
        }        
    }
}