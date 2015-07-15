using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository specializationRepository)
        {
            _unitOfWork = unitOfWork;
            _specializationRepository = specializationRepository;
        }

        public Task<IEnumerable<Specialization>> GetSpecializationsAsync()
        {
            return _specializationRepository.GetAllAsync();
        }

        public Task<Specialization> GetByIdAsync(int id)
        {
            return _specializationRepository.GetByIdAsync(id);
        }

        public async Task<Specialization> UpdateAsync(Specialization specialization)
        {
            _specializationRepository.Update(specialization);
            await _unitOfWork.SaveChangesAsync();
            return specialization;
        }

        public async Task<Specialization> CreateAsync(Specialization specialization)
        {
            _specializationRepository.Insert(specialization);
            await _unitOfWork.SaveChangesAsync();
            return specialization;
        }
    }
}