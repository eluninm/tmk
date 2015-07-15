using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecommendationRepository _recommendationRepository;


        public RecommendationService(IUnitOfWork unitOfWork, IRecommendationRepository recommendationRepository)
        {
            _unitOfWork = unitOfWork;
            _recommendationRepository = recommendationRepository;
        }

        public async Task<Recommendation> CreateAsync(Recommendation recommendation)
        {
            _recommendationRepository.Insert(recommendation);
            await _unitOfWork.SaveChangesAsync();
            return recommendation;
        }

        public async Task<IEnumerable<Recommendation>> GetAllAsync()
        {
            return await _recommendationRepository.GetAllAsync();
        }


        public async Task<Recommendation> GetByIdAsync(int id)
        {
            Recommendation recommendation = await _recommendationRepository.GetByIdAsync(id);
            return recommendation;
        }

        public async Task<Recommendation> UpdateAsync(Recommendation recommendation)
        {
            _recommendationRepository.Update(recommendation);
            await _unitOfWork.SaveChangesAsync();
            return recommendation;
        }

        public async Task<Recommendation> AddAttachment(Attachment attachment, Recommendation recommendation)
        {
            recommendation.Attachments.Add(attachment);
            await _unitOfWork.SaveChangesAsync();
            return recommendation;
        }

        public void Delete(Recommendation recommendation)
        {
            _recommendationRepository.Delete(recommendation);
            _unitOfWork.SaveChangesAsync();
        }

        public IEnumerable<Recommendation> GetAll()
        {
            return _recommendationRepository.GetAll();
        }

        public Task<IEnumerable<Recommendation>> GetPatientRecommendations(int patientId)
        {
            return _recommendationRepository.GetPatientRecommendations(patientId);
        }
    }
}