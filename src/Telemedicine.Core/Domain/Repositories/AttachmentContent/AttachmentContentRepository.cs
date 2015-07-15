using Telemedicine.Core.Data;
using Telemedicine.Core.Data.EntityFramework;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Repositories
{
    public class AttachmentContentRepository : EfRepositoryBase<AttachmentContent>, IAttachmentContentRepository
    {
        public AttachmentContentRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}