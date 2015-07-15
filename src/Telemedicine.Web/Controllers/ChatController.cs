using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IConversationService _messageService;

        public ChatController(IConversationService messageService)
        {
            _messageService = messageService;
        }

        //public async Task<JsonResult> Index()
        //{
        //    var msgs = await _messageService.GetLastMessages(HttpContext.User.Identity.GetUserId());
        //    var result = msgs.Select(t => new TextChatMessage
        //    {
        //        Message = t.Message,
        //        UserName = t.Creator.UserName,
        //        UserDisplayName = t.Creator.DisplayName
        //    }).ToList();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}