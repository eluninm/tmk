using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Extensions;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Areas.Administrator.Models;

namespace Telemedicine.Web.Areas.Administrator.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly SiteUserManager _userManager;

        public PatientController(IPatientService patientService, SiteUserManager userManager)
        {
            _patientService = patientService;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var patients = await _patientService.GetAllAsync();

            var patientsModel = patients.Select(p => new PatientViewModel
            {
                Id = p.Id,
                Email = p.User.Email,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                MiddleName = p.User.MiddleName,
                Age = p.Age,
                Sex = p.Sex
            });

            return View(patientsModel);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PatientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new SiteUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    UserName = model.Email,
                    Email = model.Email,
                    AvatarUrl = model.AvatarUrl
                };

                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user.Id, UserRoleNames.Patient);
                    if (roleResult.Succeeded)
                    {
                        var patient = new Core.Models.Patient();
                        patient.UserId = user.Id;
                        patient.Age = model.Age;
                        patient.Sex = model.Sex;
                        await _patientService.CreateAsync(patient);
                        return RedirectToAction("Index");
                    }

                    roleResult.Errors.ForEach(t => ModelState.AddModelError(string.Empty, t));
                }

                foreach (var identityError in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError);
                }

            }

            return View(model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public async Task<ActionResult> Edit(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return HttpNotFound("patient not found");
            }

            var doctorViewModel = new PatientChangeViewModel
            {
                Id = patient.Id,
                Email = patient.User.Email,
                FirstName = patient.User.FirstName,
                LastName = patient.User.LastName,
                MiddleName = patient.User.MiddleName,
                Age = patient.Age,
                Sex = patient.Sex,
                AvatarUrl = patient.User.AvatarUrl
            };

            return View(doctorViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, PatientChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = await _patientService.GetByIdAsync(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }

                var user = await _userManager.FindByIdAsync(patient.UserId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.Email = model.Email;
                user.AvatarUrl = model.AvatarUrl;

                patient.Sex = model.Sex;
                patient.Age = model.Age;

                var identityResult = await _userManager.UpdateAsync(user);
                if (identityResult.Succeeded)
                {
                    await _patientService.UpdateAsync(patient);

                    return RedirectToAction("Index");
                }

                foreach (var identityError in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError);
                }
            }

            return View(model);
        }
    }
}