using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class AttachmentContentService : IAttachmentContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentContentRepository _contentRepository;

        public AttachmentContentService(IUnitOfWork unitOfWork, IAttachmentContentRepository contentRepository)
        {
            _unitOfWork = unitOfWork;
            _contentRepository = contentRepository;
        }


        public async Task<IEnumerable<AttachmentContent>> GetAllAsync()
        {
            return await _contentRepository.GetAllAsync();
        }

        public async Task<AttachmentContent> CreateAsync(AttachmentContent attachmentContent)
        {
            _contentRepository.Insert(attachmentContent);
            await _unitOfWork.SaveChangesAsync();
            return attachmentContent;
        }
        
        public async Task<AttachmentContent> GetByIdAsync(int id)
        {
            AttachmentContent attachmentContent = await _contentRepository.GetByIdAsync(id);
            return attachmentContent; 
        }

        public async Task<AttachmentContent> UpdateAsync(AttachmentContent attachmentContent)
        {
            _contentRepository.Update(attachmentContent);
            await _unitOfWork.SaveChangesAsync();
            return attachmentContent;
        }
        
        public void Delete(AttachmentContent attachmentContent)
        {
            _contentRepository.Delete(attachmentContent);
            _unitOfWork.SaveChangesAsync();
        }
    }
}