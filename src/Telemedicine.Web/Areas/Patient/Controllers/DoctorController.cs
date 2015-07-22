using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Areas.Patient.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = UserRoleNames.Patient)]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public DoctorController(
            IDoctorService doctorService, 
            IPatientService patientService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public async Task<ActionResult> List()
        {
            ViewBag.Balance = (await _patientService.GetByUserIdAsync(User.Identity.GetUserId()))?.Balance;
            return View();
        }

        public async Task<ActionResult> StartDialog(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            var viewModel = new ChatDialogViewModel
            {
                DoctorDisplayName = doctor.User.DisplayName,
                DoctorUserId = doctor.UserId,
                Specialization = doctor.Specialization.DisplayName
            };
            
            return PartialView("_WaitDoctorAnswerDialog", viewModel);
        }
    }
}