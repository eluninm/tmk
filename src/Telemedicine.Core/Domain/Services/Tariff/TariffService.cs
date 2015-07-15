using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class TariffService : ITariffService
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TariffService(ITariffRepository tariffRepository, IUnitOfWork unitOfWork)
        {
            _tariffRepository = tariffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tariff>> GetAllAsync()
        {
            return await _tariffRepository.GetAllAsync();
        }

        public async Task<Tariff> CreateAsync(Tariff tariff)
        {
            Tariff newTariff = _tariffRepository.Insert(tariff);
            await _unitOfWork.SaveChangesAsync();
            return newTariff;
        }

        public async Task<Tariff> GetByIdAsync(int id)
        {
            Tariff newTariff = await _tariffRepository.GetByIdAsync(id);
            return newTariff;
        }

        public async Task<Tariff> UpdateAsync(Tariff tariff)
        {
            Tariff newTariff = _tariffRepository.Update(tariff);
            await _unitOfWork.SaveChangesAsync();
            return newTariff;
        }
    }
}