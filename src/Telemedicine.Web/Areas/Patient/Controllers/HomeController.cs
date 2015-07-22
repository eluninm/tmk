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

        public async Task<ActionResult> Index()
        { 
            var patient = await _patientService.GetByUserIdAsync(HttpContext.User.Identity.GetUserId());
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = patient.Id;

            return View();
        }


        public async Task<ActionResult> MyEvents()
        {
            ViewBag.Balance = (await _patientService.GetByUserIdAsync(User.Identity.GetUserId()))?.Balance;
            return View();
        }
    }
}