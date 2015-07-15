using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            var newPayment= _paymentRepository.Insert(payment);
            await _unitOfWork.SaveChangesAsync();
            return newPayment;
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            Payment payment = await _paymentRepository.GetByIdAsync(id);
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            var updatedPayment =  _paymentRepository.Update(payment);
            await _unitOfWork.SaveChangesAsync();
            return updatedPayment;
        }

        public async Task<Payment> GetByPatientIdAsync(int id)
        {
            return await _paymentRepository.GetByPatientIdAsync(id);
        }

        public Payment GetByPatientId(int id)
        {
            return _paymentRepository.GetByPatientId(id);
        }
    }
}