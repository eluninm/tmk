using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Areas.Patient.Models;
using System.Web.Script.Serialization;
using Telemedicine.Core.Domain.Consts;
using System.Data.Entity.Validation;
using System.Runtime.Remoting.Contexts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telemedicine.Core.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = UserRoleNames.Patient)]
    public class HomeController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly SiteUserManager _userManager;
        private readonly IDoctorService _doctorService;
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IPaymentService _paymentService;
        private readonly IUserEventsService _userEventsService;
        private readonly IRecommendationService _recommendationService;

        public HomeController(
            IPatientService patientService
            , SiteUserManager userManager
            , IDoctorService doctorService
            , ITimeSpanEventService timeSpanEventService
            , IPaymentService paymentService
            , IUserEventsService userEventsService, IRecommendationService recommendationService)
        {
            _patientService = patientService;
            _userManager = userManager;
            _doctorService = doctorService;
            _timeSpanEventService = timeSpanEventService;
            _paymentService = paymentService;
            _userEventsService = userEventsService;
            _recommendationService = recommendationService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index2()
        {
            var patient = await _patientService.GetByUserIdAsync(HttpContext.User.Identity.GetUserId());
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = patient.Id;

            return View();
        }


        public ActionResult MyEvents()
        {
            return View();
        }

        public async Task<ActionResult> Index_()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var patient = await _patientService.GetByUserIdAsync(user.Id);

            ViewBag.Balance = patient.Balance;
            
            ViewBag.PaymentInstrumentEmpty = user.PaymentInstrument;
            ViewBag.FirstLogin = user.LastLoginDate == null;

            var doctors = await _doctorService.GetAllAsync();
            ViewBag.Doctors = JsonConvert.SerializeObject(doctors.ToList());

            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return View();
        }

        public async Task<ActionResult> GetDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SetDoctor(string siteUserId, string date)
        {
            var patientSiteUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var patient = await _patientService.GetByUserIdAsync(patientSiteUser.Id);

            patient.Balance -= 500;
            await _patientService.UpdateAsync(patient);

            return Json("ok", JsonRequestBehavior.DenyGet);
        }

        public async Task<ActionResult> GetEvents()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var events = await _timeSpanEventService.GetAllAsync();

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetBalance()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var patient = await _patientService.GetByUserIdAsync(user.Id);

            return Json(patient.Balance, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetRecommendationDetail(int id)
        {
            Recommendation recommendation = await _recommendationService.GetByIdAsync(id);
            Core.Models.Doctor doctor = await _doctorService.GetByIdAsync(recommendation.DoctorId);
            RecommendationDetailViewModel viewModel = new RecommendationDetailViewModel()
            {
                Content = recommendation?.RecommendationText,
                Date = recommendation?.CreateDate ?? new DateTime(),
                DoctorFIO = doctor?.User?.DisplayName,
                SpecializationDisplayName = doctor?.Specialization?.DisplayName
            };

            return PartialView("_RecommendationDetailDialog", viewModel);
        }
    }
}