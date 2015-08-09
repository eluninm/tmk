using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public class DoctorPaymentService : IDoctorPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorPaymentRepository _doctorPaymentRepository;

        public DoctorPaymentService(IDoctorPaymentRepository doctorPaymentRepository, IUnitOfWork unitOfWork)
        {
            _doctorPaymentRepository = doctorPaymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoctorPaymentHistory>> GetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<DoctorPaymentHistory> CreateAsync(DoctorPaymentHistory doctorPaymentHistory)
        {
            DoctorPaymentHistory newEntity = _doctorPaymentRepository.Insert(doctorPaymentHistory);
            await _unitOfWork.SaveChangesAsync();
            return newEntity;
        }

        public async Task<DoctorPaymentHistory> GetByIdAsync(int id)
        {
            return await _doctorPaymentRepository.GetByIdAsync(id);
        }

        public async Task<DoctorPaymentHistory> UpdateAsync(DoctorPaymentHistory doctorPaymentHistory)
        {
            DoctorPaymentHistory newEntity = _doctorPaymentRepository.Update(doctorPaymentHistory);
            await _unitOfWork.SaveChangesAsync();
            return newEntity;
        }

        public async Task<IPagedList<DoctorPaymentHistory>> PagedAsync(int id, int? page, int? pageSize, DateTime? start, DateTime? end)
        {
            return await _doctorPaymentRepository.PagedAsync(id, page, pageSize, start, end);
        }
    }
}