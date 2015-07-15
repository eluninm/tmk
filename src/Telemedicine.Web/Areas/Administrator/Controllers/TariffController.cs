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
    public class TariffController : Controller
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<TariffViewModel> methods = (await _tariffService.GetAllAsync())
                .Select(item => new TariffViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Cost = item.Cost,
                    Messages = item.Messages,
                    Minutes = item.Minutes 
                });
            return View(methods);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TariffViewModel method)
        {
            if (ModelState.IsValid)
            {
                await
                    _tariffService.CreateAsync(new Tariff()
                    {
                        Name = method.Name,
                        Cost = method.Cost, 
                        Messages = method.Messages,
                        Minutes = method.Minutes
                    });
                return RedirectToAction("Index");
            }
            return View(method);
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var tariff = await _tariffService.GetByIdAsync(id);
            if (tariff == null)
            {
                return HttpNotFound("tariff not found");
            }

            var tariffViewModel = new TariffViewModel()
            {
                Id = tariff.Id,
                Name = tariff.Name,
                Cost = tariff.Cost,
                Messages = tariff.Messages,
                Minutes = tariff.Minutes
            };
            return View(tariffViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TariffViewModel tariffViewModel)
        {
            if (ModelState.IsValid)
            {
                var tariff = await _tariffService.GetByIdAsync(tariffViewModel.Id);
                if (tariff == null)
                {
                    return HttpNotFound("tariff not found");
                }

                tariff.Cost = tariffViewModel.Cost;
                tariff.Name = tariffViewModel.Name;
                tariff.Messages = tariffViewModel.Messages;
                tariff.Minutes = tariffViewModel.Minutes;

                await _tariffService.UpdateAsync(tariff);
                return RedirectToAction("Index");
            }
            return View(tariffViewModel);
        }
    }
}