using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class SpecializationRepository : EfRepositoryBase<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}