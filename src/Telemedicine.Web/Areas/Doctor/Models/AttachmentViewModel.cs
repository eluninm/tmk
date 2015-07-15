using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Telemedicine.Web.Areas.Doctor.Models
{
    public class AttachmentViewModel
    { 
        public string Description { get; set; } 

        public HttpPostedFileBase File { get; set; } 

    }
}