using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public interface IAttachmentContentService
    {
        Task<IEnumerable<AttachmentContent>> GetAllAsync();

        Task<AttachmentContent> CreateAsync(AttachmentContent attachmentContent);

         Task<AttachmentContent> GetByIdAsync(int id);

        Task<AttachmentContent> UpdateAsync(AttachmentContent attachmentContent);

        void Delete(AttachmentContent attachmentContent);
    }
}