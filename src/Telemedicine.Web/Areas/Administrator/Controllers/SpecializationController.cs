using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Web.Areas.Administrator.Models;

namespace Telemedicine.Web.Areas.Administrator.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        public async Task<ActionResult> Index()
        {
            var specializationsModel = (await _specializationService.GetSpecializationsAsync())
                .Select(t => new SpecializationViewModel
                {
                    Id = t.Id,
                    Code = t.Code,
                    DisplayName = t.DisplayName
                });
                
            return View(specializationsModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(SpecializationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialization = new Specialization
                {
                    Code = model.Code,
                    DisplayName = model.DisplayName
                };

                await _specializationService.CreateAsync(specialization);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return HttpNotFound("specialization not found");
            }

            var specializationViewModel = new SpecializationViewModel
            {
                Id = specialization.Id,
                Code = specialization.Code,
                DisplayName = specialization.DisplayName
            };

            return View(specializationViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, SpecializationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialization = await _specializationService.GetByIdAsync(id);
                if (specialization == null)
                {
                    return HttpNotFound("specialization not found");
                }

                specialization.Code = model.Code;
                specialization.DisplayName = model.DisplayName;
                await _specializationService.UpdateAsync(specialization);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}