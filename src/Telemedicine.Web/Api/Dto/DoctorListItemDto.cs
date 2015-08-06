using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemedicine.Web.Api.Dto
{
    public class DoctorListItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public string Specialization { get; set; }

        public string AvatarUrl { get; set; }

        public string StatusText { get; set; }

        public string StatusName { get; set; }

        public bool IsChatAvailable { get; set; }

        public bool IsAudioAvailable { get; set; }

        public bool IsVideoAvailable { get; set; }
    }
}