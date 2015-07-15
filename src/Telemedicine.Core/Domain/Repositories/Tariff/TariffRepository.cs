using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class TariffRepository :EfRepositoryBase<Tariff>, ITariffRepository
    {
        public TariffRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}