using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>>  GetAllAsync();

        Task<Patient> CreateAsync(Patient patient);

        Task<Patient> GetByIdAsync(int id);

        Task<Patient> UpdateAsync(Patient patient);

        Task<Patient> GetByUserIdAsync(string id);

        Patient GetByUserId(string userId);

        Task Replenish(int patientId, double amount);
    }
}
