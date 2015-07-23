using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentHistoryService(IUnitOfWork unitOfWork, IPaymentHistoryRepository paymentHistoryRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        public Task<IEnumerable<PaymentHistory>> GetAllAsync()
        {
            return _paymentHistoryRepository.GetAllAsync();
        }

        public async Task<PaymentHistory> CreateAsync(PaymentHistory paymentHistory)
        {
            var newPaymentHistory = _paymentHistoryRepository.Insert(paymentHistory);
            await _unitOfWork.SaveChangesAsync();
            return newPaymentHistory;
        }

        public Task<PaymentHistory> GetByIdAsync(int id)
        {
            return _paymentHistoryRepository.GetByIdAsync(id);
        }

        public async Task<PaymentHistory> UpdateAsync(PaymentHistory paymentHistory)
        {
            var newPaymentHistory = _paymentHistoryRepository.Update(paymentHistory);
            await _unitOfWork.SaveChangesAsync();
            return newPaymentHistory;
        }

        public IEnumerable<PaymentHistory> GetByPatientIdAsync(int id)
        {
            return _paymentHistoryRepository.GetByPatientIdAsync(id);
        }

        public async Task<IPagedList<PaymentHistory>> PagedAsync(string id, int page, int pageSize)
        {
            return await _paymentHistoryRepository.PagedAsync(id, page, pageSize);
        }
    }
}