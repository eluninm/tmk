using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IUserEventsService
    {
        Task<IEnumerable<UserEvent>> GetAllAsync();

        Task<UserEvent> CreateAsync(UserEvent userEvent);
        Task<UserEvent> UpdateAsync(UserEvent newUserEvent);
    }
}
