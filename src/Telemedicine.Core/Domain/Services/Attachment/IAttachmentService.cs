using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IAttachmentService
    {
        Task<IEnumerable<Attachment>> GetAllAsync();

        Task<Attachment> CreateAsync(Attachment attachment);

        Task<Attachment> GetByIdAsync(int id);

        Task<Attachment> UpdateAsync(Attachment attachment);

        void Delete(Attachment attachment);
    }
}