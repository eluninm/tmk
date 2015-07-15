using System.Collections.Generic;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IUnitOfWork unitOfWork, IAttachmentRepository attachmentRepository)
        {
            _unitOfWork = unitOfWork;
            _attachmentRepository = attachmentRepository;
        }

        public Task<IEnumerable<Attachment>> GetAllAsync()
        {
            return _attachmentRepository.GetAllAsync();
        }

        public async Task<Attachment> CreateAsync(Attachment attachment)
        {
            _attachmentRepository.Insert(attachment);
            await _unitOfWork.SaveChangesAsync();
            return attachment;
        }

        public async Task<Attachment> GetByIdAsync(int id)
        {
            return await _attachmentRepository.GetByIdAsync(id);
        }

        public async Task<Attachment> UpdateAsync(Attachment attachment)
        {
            _attachmentRepository.Update(attachment);
            await _unitOfWork.SaveChangesAsync();
            return attachment;
        }

        public void Delete(Attachment calendarEvent)
        {
            _attachmentRepository.Delete(calendarEvent);
            _unitOfWork.SaveChanges();
        }
    }
}