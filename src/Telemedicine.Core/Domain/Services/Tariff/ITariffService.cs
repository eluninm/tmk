using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface ITariffService
    {
        Task<IEnumerable<Tariff>> GetAllAsync();

        Task<Tariff> CreateAsync(Tariff tariff);

        Task<Tariff> GetByIdAsync(int id);

        Task<Tariff> UpdateAsync(Tariff tariff);
    }
}