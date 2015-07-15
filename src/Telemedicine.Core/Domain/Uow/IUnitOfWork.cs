using System.Threading.Tasks;

namespace Telemedicine.Core.Domain.Uow
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// </summary>
        Task SaveChangesAsync();
    }
}
