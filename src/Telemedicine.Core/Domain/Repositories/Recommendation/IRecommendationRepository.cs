using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public interface IRecommendationRepository:IRepository<Recommendation>
    {
        IEnumerable<Recommendation> GetAll();
        Task<IEnumerable<Recommendation>> GetPatientRecommendations(int patientId);
    }
}