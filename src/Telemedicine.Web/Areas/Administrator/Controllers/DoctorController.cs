using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Extensions;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Areas.Administrator.Models;
using Telemedicine.Core.Models;

namespace Telemedicine.Web.Areas.Administrator.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly SiteUserManager _userManager;

        public DoctorController(IDoctorService doctorService, SiteUserManager userManager)
        {
            _doctorService = doctorService;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var doctors = await _doctorService.GetAllAsync();

            var doctorsModel = doctors.Select(doctor => new DoctorViewModel
            {
                Id = doctor.Id,
                Email = doctor.User.Email,
                FirstName = doctor.User.FirstName,
                LastName = doctor.User.LastName,
                MiddleName = doctor.User.MiddleName,
                Specialization = doctor.Specialization.DisplayName
            });

            return View(doctorsModel);
        }

        public async Task<ActionResult> Create()
        {
            var specializations = await _doctorService.GetSpecializationsAsync();
            ViewBag.Specializations = specializations.Select(t => new SelectListItem
            {
                Text = t.DisplayName,
                Value = t.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(DoctorCreateViewModel model)
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
                    var roleResult = await _userManager.AddToRoleAsync(user.Id, UserRoleNames.Doctor);
                    if (roleResult.Succeeded)
                    {
                        var doctor = new Core.Models.Doctor();
                        doctor.UserId = user.Id;
                        doctor.SpecializationId = model.SpecializationId;
                        doctor.Description = model.Description;
                        await _doctorService.CreateAsync(doctor);

                        return RedirectToAction("Index");
                    }

                    roleResult.Errors.ForEach(t => ModelState.AddModelError(string.Empty, t));
                }

                identityResult.Errors.ForEach(t => ModelState.AddModelError(string.Empty, t));
            }

            var specializations = await _doctorService.GetSpecializationsAsync();
            ViewBag.Specializations = specializations.Select(t => new SelectListItem
            {
                Text = t.DisplayName,
                Value = t.Id.ToString()
            });

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return HttpNotFound("doctor not found");
            }

            var specializations = await _doctorService.GetSpecializationsAsync();
            ViewBag.Specializations = specializations.Select(t => new SelectListItem
            {
                Text = t.DisplayName,
                Value = t.Id.ToString(),
                Selected = t.Id == doctor.SpecializationId
            });

            var doctorViewModel = new DoctorChangeViewModel
            {
                Id = doctor.Id,
                Email = doctor.User.Email,
                FirstName = doctor.User.FirstName,
                LastName = doctor.User.LastName,
                MiddleName = doctor.User.MiddleName,
                SpecializationId = doctor.SpecializationId,
                Description = doctor.Description
            }; 

            return View(doctorViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, DoctorChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _doctorService.GetByIdAsync(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }

                var user = await _userManager.FindByIdAsync(doctor.UserId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.Email = model.Email;
                user.AvatarUrl = model.AvatarUrl;

                var identityResult = await _userManager.UpdateAsync(user);
                if (identityResult.Succeeded)
                {
                    doctor.SpecializationId = model.SpecializationId;
                    doctor.Description = model.Description;
                    await _doctorService.UpdateAsync(doctor);

                    return RedirectToAction("Index");
                }

                foreach (var identityError in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError);
                }
            }

            var specializations = await _doctorService.GetSpecializationsAsync();
            ViewBag.Specializations = specializations.Select(t => new SelectListItem
            {
                Text = t.DisplayName,
                Value = t.Id.ToString()
            });

            return View(model);
        }
    }
}