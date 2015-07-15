using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUnitOfWork _unitOfWorkl;
        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IUnitOfWork unitOfWorkl)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _unitOfWorkl = unitOfWorkl;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _paymentMethodRepository.GetAllAsync();
        }

        public async Task<PaymentMethod> CreateAsync(PaymentMethod paymentMethod)
        {
            PaymentMethod method = _paymentMethodRepository.Insert(paymentMethod);
            await _unitOfWorkl.SaveChangesAsync();
            return method;
        }

        public async Task<PaymentMethod> GetByIdAsync(int id)
        {
            return await _paymentMethodRepository.GetByIdAsync(id);
        }

        public async Task<PaymentMethod> UpdateAsync(PaymentMethod paymentMethod)
        {
            PaymentMethod method =  _paymentMethodRepository.Update(paymentMethod);
            await _unitOfWorkl.SaveChangesAsync();
            return method;
        }
    }
}