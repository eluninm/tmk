using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly IPaymentHistoryService _paymentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public ConversationController(IConversationService conversationService, IPaymentHistoryService paymentService, IPatientService patientService, IDoctorService doctorService)
        {
            _conversationService = conversationService;
            _paymentService = paymentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        //[HttpPost]
        //public async Task<ActionResult> Create(string targetUserId)
        //{
        //    var creatorId = HttpContext.User.Identity.GetUserId();
        //    var conversation = await _conversationService.BeginConversation(creatorId, targetUserId);

        //    var patient = await _patientService.GetByUserIdAsync(targetUserId);

        //    await _paymentService.CreateAsync(new PaymentHistory
        //    {
        //        ConversationId = conversation.Id,
        //        Date = conversation.Created,
        //        Patient = patient,
        //        PaymentType = PaymentType.Consultation,
        //        Value = -500
        //    });

        //    return Json(new { ConversationId = conversation.Id });
        //}

        public async Task<ActionResult> Open(string id)
        { 
            var conversation = await _conversationService.OpenConversation(id);
            if (conversation == null)
            {
                return HttpNotFound("An error has ocurred while conversation opening");
            }

            var currentUserId = HttpContext.User.Identity.GetUserId();
            var targetUser = conversation.Members.FirstOrDefault(t => t.Id != currentUserId);

            if (targetUser == null)
            {
                return HttpNotFound("Conversation doesnt contain target member");
            }

            ViewBag.UserDisplayName = targetUser.DisplayName;
            ViewBag.UserAvatarUrl = targetUser.AvatarUrl;
            ViewBag.TargetUserId = targetUser.Id;
            ViewBag.ConversationId = conversation.Id;

            return View();
        }
    }
}