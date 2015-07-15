using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Areas.Patient.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly IRecommendationService _recommendationService;
        private readonly IPatientService _patientService;
        private readonly SiteUserManager _userManager;
        private readonly IDoctorService _doctorService;

        public HistoryController(IConversationService conversationService, IRecommendationService recommendationService, IPatientService patientService, SiteUserManager userManager, IDoctorService doctorService)
        {
            _conversationService = conversationService;
            _recommendationService = recommendationService;
            _patientService = patientService;
            _userManager = userManager;
            _doctorService = doctorService;
        }

        public async Task<ActionResult> Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var patient = await _patientService.GetByUserIdAsync(userId);
            var recomeds = await _recommendationService.GetPatientRecommendations(patient.Id);
            var conversations = await _conversationService.GetUserConversations(userId);

            var list = new List<MedicalHistoryViewModel>();
            foreach (var rec in recomeds)
            {
                list.Add(new MedicalHistoryViewModel
                {
                    Date = rec.CreateDate,
                    Text = rec.RecommendationText,
                    Type = "rec",
                    TargetId = rec.Id.ToString()
                });
            }
            //foreach (var con in conversations)
            //{
            //    var doctorUser = con.Members.FirstOrDefault(t => t.Id != userId);

            //    list.Add(new MedicalHistoryViewModel
            //    {
            //        Date = con.Created,
            //        Text = $"Консультация с {doctorUser.DisplayName}",
            //        Type = "rec",
            //        TargetId = con.Id.ToString()
            //    });
            //}

            return View(list);
        }
    }
}