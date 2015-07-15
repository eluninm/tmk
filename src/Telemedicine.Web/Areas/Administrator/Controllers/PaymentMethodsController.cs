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
    [Authorize]
    public class PaymentMethodsController : Controller
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }


        // GET: Administrator/PaymentMethods
        public async Task<ActionResult> Index()
        {
            IEnumerable<PaymentMethodViewModel> methods = (await _paymentMethodService.GetAllAsync())
                .Select(item => new PaymentMethodViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            return View(methods);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PaymentMethodViewModel method)
        {
            if (ModelState.IsValid)
            {
                await _paymentMethodService.CreateAsync(new PaymentMethod() {Name = method.Name});
                return RedirectToAction("Index");
            }
            return View(method);
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var paymentMethod = await _paymentMethodService.GetByIdAsync(id);
            if (paymentMethod == null)
            {
                return HttpNotFound("PaymentMethod not found");
            }

            var paymentMethodViewModel = new PaymentMethodViewModel()
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name
            };
            return View(paymentMethodViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PaymentMethodViewModel method)
        {
            if (ModelState.IsValid)
            {
                var paymentMethod = await _paymentMethodService.GetByIdAsync(method.Id);
                if (paymentMethod == null)
                {
                    return HttpNotFound("PaymentMethod not found");
                }

                paymentMethod.Name = method.Name;
                await _paymentMethodService.UpdateAsync(paymentMethod);
                return RedirectToAction("Index");
            }
            return View(method);
        }
    }
}