using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;

namespace Telemedicine.Core.Domain.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorStatusRepository _doctorStatusRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecializationRepository _specializationRepository;

        public DoctorService(
            IUnitOfWork unitOfWork, 
            IDoctorStatusRepository doctorStatusRepository, 
            IDoctorRepository doctorRepository,
            ISpecializationRepository specializationRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorStatusRepository = doctorStatusRepository;
            _doctorRepository = doctorRepository;
            _specializationRepository = specializationRepository;
        }

        #region Doctor statuses

        /// <summary>
        /// Returns current status of a doctor.
        /// </summary>
        /// <param name="doctorId">Identifier of a doctor object.</param>
        /// <returns>Current status if exists or 'Busy' if not set.</returns>
        public DoctorStatus GetStatusById(int doctorId)
        {
            var doctor = _doctorRepository.GetById(doctorId);

            if (doctor == null)
            {
                throw new Exception(Resources.DoctorNotFound);
            }

            return _doctorStatusRepository.GetById(doctor.DoctorStatusId)
                   ?? _doctorStatusRepository.GetByName(DoctorStatusNames.Busy);
        }

        /// <summary>
        /// Returns current status of a doctor.
        /// </summary>
        /// <param name="userId">Identifier of a correponding user object.</param>
        /// <returns>Current status if exists or 'Busy' if not set.</returns>
        public DoctorStatus GetStatusByUserId(string userId)
        {
            var doctor = _doctorRepository.GetByUserId(userId);
            if (doctor == null)
            {
                throw new Exception(Resources.DoctorNotFound);
            }

            return _doctorStatusRepository.GetById(doctor.DoctorStatusId)
                ?? _doctorStatusRepository.GetByName(DoctorStatusNames.Busy);
        }

        /// <summary>
        /// Sets status of a doctor.
        /// </summary>
        /// <param name="doctorId">Identifier of a doctor.</param>
        /// <param name="statusId">Identifier of a status.</param>
        /// <returns>Setted status.</returns>
        public DoctorStatus SetStatusByDoctorId(int doctorId, int statusId)
        {
            var doctor = _doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                throw new Exception(Resources.DoctorNotFound);
            }

            var status = _doctorStatusRepository.GetById(statusId);
            if (status == null)
            {
                throw new Exception(Resources.DoctorStatusNotFound);
            }

            doctor.DoctorStatus = status;
            _unitOfWork.SaveChanges();

            return status;
        }

        public DoctorStatus SetStatusByUserId(string userId, int statusId)
        {
            var doctor = _doctorRepository.GetByUserId(userId);
            if (doctor == null)
            {
                throw new Exception(Resources.DoctorNotFound);
            }

            var status = _doctorStatusRepository.GetById(statusId);
            if (status == null)
            {
                throw new Exception(Resources.DoctorStatusNotFound);
            }

            doctor.DoctorStatus = status;
            _unitOfWork.SaveChanges();

            return status;
        }

        /// <summary>
        /// Get all available statuses.
        /// </summary>
        public Task<IEnumerable<DoctorStatus>> GetAvailableStatusesAsync()
        {
            return _doctorStatusRepository.AvailableStatusesAsync();
        }

        #endregion

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return _doctorRepository.GetAllAsync();
        }

        public Task<IPagedList<Doctor>> PagedAsync(int page = 1, int pageSize = 10, string titleFilter = null, int specializationFilter = 0)
        {
            return _doctorRepository.PagedAsync(page, pageSize, titleFilter, specializationFilter);
        }

        public  IEnumerable<Doctor>  GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public Task<IEnumerable<Specialization>> GetSpecializationsAsync()
        {
            return _specializationRepository.GetAllAsync();
        }

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            doctor.DoctorStatus = await _doctorStatusRepository.GetByNameAsync(DoctorStatusNames.Busy);
            _doctorRepository.Insert(doctor);
            _unitOfWork.SaveChanges();
            return doctor;
        }

        public Task<Doctor> GetByIdAsync(int id)
        {
            return _doctorRepository.GetByIdAsync(id);
        }

        public async Task<Doctor> GetByUserIdAsync(string id)
        {
            return await _doctorRepository.GetByUserIdAsync(id);
        }

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            _doctorRepository.Update(doctor);
            await _unitOfWork.SaveChangesAsync();
            return doctor;
        }

        public IEnumerable<DoctorStatus> GetAvailableStatuses()
        {
            return _doctorStatusRepository.GetAvailableStatuses();
        }

        public Doctor GetById(int id)
        {
            return _doctorRepository.GetById(id);
        }
    }
}