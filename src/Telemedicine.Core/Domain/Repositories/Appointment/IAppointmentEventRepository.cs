using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IAppointmentEventRepository : IRepository<AppointmentEvent>
    {

    }
}
