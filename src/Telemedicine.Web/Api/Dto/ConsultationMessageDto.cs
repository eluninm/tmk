using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class ConsultationMessageDto
    {
        public string CreatorId { get; set; }

        public string UserDisplayName { get; set; }

        public string UserName { get; set; }

        public string Message { get; set; }

        public string Direction { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime Created { get; set; }
    }
}