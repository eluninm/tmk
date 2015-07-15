using System.Threading.Tasks;
using Telemedicine.Core.Domain.Uow;

namespace Telemedicine.Core.Data.EntityFramework
{
    public class EfSimpleUnitOfWork : IUnitOfWork
    {
        private readonly IDbContextProvider _dbContextProvider;

        public EfSimpleUnitOfWork(IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public void SaveChanges()
        {
            _dbContextProvider.Context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _dbContextProvider.Context.SaveChangesAsync();
        }
    }
}
