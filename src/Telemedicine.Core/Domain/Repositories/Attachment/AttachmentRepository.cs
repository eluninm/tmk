using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class AttachmentRepository :EfRepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}