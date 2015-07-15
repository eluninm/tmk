using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class AppointmentEventRepository : EfRepositoryBase<AppointmentEvent>, IAppointmentEventRepository
    {
        public AppointmentEventRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public async override Task<IEnumerable<AppointmentEvent>> GetAllAsync()
        {
            return Set.Include(t => t.Patient).Include(p=>p.Patient.User)
                .Include(t => t.Doctor).Include(t=>t.Doctor).Include(t => t.Doctor.User)
                .ToList();
        }
 /*
        public override  Task<AppointmentEvent> GetByIdAsync(int id)
        {
            return Set.FirstOrDefault(t => t.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }*/
         
    }
}