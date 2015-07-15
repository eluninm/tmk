using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetSpecializationsAsync();

        Task<Specialization> GetByIdAsync(int id);

        Task<Specialization> UpdateAsync(Specialization specialization);

        Task<Specialization> CreateAsync(Specialization specialization);
    }
}
