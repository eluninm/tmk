using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Areas.Doctor.Models;
using Telemedicine.Web.Areas.Patient.Models;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    public class PaymentController : Controller
    { 
        private readonly IPaymentMethodService _paymentMethodService; 
        private readonly IDoctorService _doctorService;
        public PaymentController(IPaymentMethodService paymentMethodService,IDoctorService doctorService)
        {
            _paymentMethodService = paymentMethodService; 
            _doctorService = doctorService;  
        }

        public async Task<ActionResult> Payment()
        { 
            IEnumerable<PaymentMethod> methods = await _paymentMethodService.GetAllAsync();
            PaymentViewModel viewModel = new PaymentViewModel();
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());

            viewModel.PaymentMethodListItems = from method in methods
                                               select new SelectListItem() { Text = method.Name, Value = method.Id.ToString(), Selected = (method.Id == (doctor?.PaymentMethod?.Id)) };
             
            viewModel.SelectedMethodId = (doctor?.PaymentMethod?.Id)??-1;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Payment(PaymentViewModel viewModel)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());

            if (doctor != null && ModelState.IsValid)
            {
                doctor.PaymentMethod =  await _paymentMethodService.GetByIdAsync(viewModel.SelectedMethodId);
                await _doctorService.UpdateAsync(doctor);
                return RedirectToAction("Index", "Home");
            } 

            IEnumerable<PaymentMethod> methods = await _paymentMethodService.GetAllAsync();

            viewModel.PaymentMethodListItems = from method in methods
                                               select new SelectListItem() { Text = method.Name, Value = method.Id.ToString(), Selected = (viewModel.SelectedMethodId == method.Id) };
            
            return View(viewModel);
        }

    }
}