using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories.DoctorWorkHours
{
    public interface IDoctorTimetableRepository : IRepository<DoctorTimetable>
    {
        Task<IEnumerable<DoctorTimetable>> GetDoctorTimetableByMonthAsync(int doctorId, int year, int month);
    }
}
