using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IRecommendationService
    {
        Task<Recommendation> CreateAsync(Recommendation recommendation);

        Task<IEnumerable<Recommendation>> GetAllAsync();

        Task<Recommendation> GetByIdAsync(int id);

        Task<Recommendation> UpdateAsync(Recommendation recommendation);

        Task<Recommendation> AddAttachment(Attachment attachment, Recommendation recommendation);

        void Delete(Recommendation recommendation);

        IEnumerable<Recommendation> GetAll();

        Task<IEnumerable<Recommendation>> GetPatientRecommendations(int patientId);
    }
}