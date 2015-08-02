using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories.DoctorWorkHours
{
    public class DoctorTimetableRepository : EfRepositoryBase<DoctorTimetable>, IDoctorTimetableRepository
    {
        public DoctorTimetableRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<DoctorTimetable>> GetDoctorTimetableByMonthAsync(int doctorId, int year, int month)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);
            return
                await
                    Set.Include(s => s.AppointmentEvents)
                        .Where(w => w.DateTime >= start && w.DateTime < end)
                        .ToListAsync();
        }
    }
}
