using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Domain.Consts;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = UserRoleNames.Patient)]
    public class HomeController : Controller
    {
        private readonly IPatientService _patientService;

        public HomeController(IPatientService patientService)
        {
            _patientService = patientService;
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
            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            ViewBag.PatientId = patient.Id;
            ViewBag.Balance = (await _patientService.GetByUserIdAsync(User.Identity.GetUserId()))?.Balance;
            return View();
        }
    }
}