using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
        }

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
            return _patientRepository.GetAllAsync();
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            _patientRepository.Insert(patient);
            await _unitOfWork.SaveChangesAsync();
            return patient;
        }

        public Task<Patient> GetByIdAsync(int id)
        {
            return _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            _patientRepository.Update(patient);
            await _unitOfWork.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> GetByUserIdAsync(string id)
        {
            return await _patientRepository.GetByUserIdAsync(id);
        }

        public Patient GetByUserId(string userId)
        {
            return _patientRepository.GetByUserId(userId);
        }
    }
}