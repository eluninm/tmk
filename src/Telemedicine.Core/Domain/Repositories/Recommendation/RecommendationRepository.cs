using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class RecommendationRepository : EfRepositoryBase<Recommendation>, IRecommendationRepository
    {
        public RecommendationRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }


        public IEnumerable<Recommendation> GetAll()
        {
            return Set;
        }

        public async Task<IEnumerable<Recommendation>> GetPatientRecommendations(int patientId, int? doctorId = null)
        {
            var query = Set.Where(t => t.PatientId == patientId)
                .Include(t => t.Doctor)
                .Include(t => t.Doctor.Specialization)
                .Include(t => t.Doctor.User);

            if (doctorId.HasValue)
            {
                query = query.Where(item => item.DoctorId == doctorId.Value);
            }
            return await query.ToListAsync();
        }
    }
}