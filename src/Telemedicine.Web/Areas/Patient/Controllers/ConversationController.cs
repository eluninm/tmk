using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Api.Dto;
using Telemedicine.Web.Areas.Patient.Models;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public ConversationController(IConversationService conversationService, IDoctorService doctorService, IPatientService patientService)
        {
            _conversationService = conversationService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public async Task<ActionResult> Open(string id)
        {
            ViewBag.Balance = (await _patientService.GetByUserIdAsync(User.Identity.GetUserId()))?.Balance;
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
            var doctor = await _doctorService.GetByUserIdAsync(targetUser.Id);
            ViewBag.Specialization = doctor.Specialization.DisplayName;
            ViewBag.Balance = "";

            var conversationViewModel = conversation.Messages.Select(t => new ConsultationMessageDto());
            return View(conversationViewModel);
        }
    }
}