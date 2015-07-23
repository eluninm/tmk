using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepository;
        private readonly IPaymentHistoryService _paymentService;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository, IPaymentHistoryService paymentService)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
            _paymentService = paymentService;
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

        public async Task Replenish(int patientId, double amount)
        {
            var patient = await GetByIdAsync(patientId);

            var payment = new PaymentHistory();
            payment.Date = DateTime.Now;
            payment.Patient = patient;
            payment.PaymentType = PaymentType.Replenishment;
            payment.Value = amount;

            patient.Balance += amount;
            await _paymentService.CreateAsync(payment);
            await UpdateAsync(patient);
        }
    }
}