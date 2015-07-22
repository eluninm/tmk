using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Areas.Patient.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = UserRoleNames.Patient)]
    public class PaymentController : Controller
    {
        private readonly IPaymentHistoryService _paymentHistoryService;
        private readonly ITariffService _tariffService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IPatientService _patientService;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentMethodService paymentMethodService, ITariffService tariffService,
            IPatientService patientService, IPaymentService paymentService, IPaymentHistoryService paymentHistoryService)
        {
            _paymentMethodService = paymentMethodService;
            _tariffService = tariffService;
            _patientService = patientService;
            _paymentService = paymentService;
            _paymentHistoryService = paymentHistoryService;
        }

        public async Task<ActionResult> Index()
        {  
            IEnumerable<Tariff> tariffs = await _tariffService.GetAllAsync();
            IEnumerable<PaymentMethod> methods = await _paymentMethodService.GetAllAsync();
            PaymentSettingsViewModel viewModel = new PaymentSettingsViewModel();

            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            var paymant = await _paymentService.GetByPatientIdAsync(patient?.Id ?? -1);

            if (paymant != null)
            {
                viewModel.SelectedMethodId = paymant?.PaymentMethod?.Id ?? -1;
                viewModel.SelectedTariffId = paymant?.Tariff?.Id ?? -1;
                viewModel.AutoPayment = paymant?.AutoPayment ?? false;
            }

            viewModel.PaymentMethodListItems = from method in methods
                select
                    new SelectListItem()
                    {
                        Text = method.Name,
                        Value = method.Id.ToString(),
                        Selected = (viewModel.SelectedMethodId == method.Id)
                    };
            viewModel.TariffListItems = from tariff in tariffs
                select
                    new SelectListItem
                    {
                        Text = tariff.Name,
                        Value = tariff.Id.ToString(),
                        Selected = (viewModel.SelectedTariffId == tariff.Id)
                    };
            viewModel.PaymentHistoryViewModel = await GetPaymentHistory();
            return View("Index2", viewModel);
        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(PaymentSettingsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
                ViewBag.Balance = patient?.Balance;
                if (patient != null)
                {
                    Payment exitsingPayment = (await _paymentService.GetByPatientIdAsync(patient?.Id ?? -1));
                    Payment payment = exitsingPayment ?? new Payment();
                    payment.Patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
                    payment.PaymentMethod = await _paymentMethodService.GetByIdAsync(viewModel.SelectedMethodId);
                    payment.Tariff = await _tariffService.GetByIdAsync(viewModel.SelectedTariffId);
                    payment.AutoPayment = viewModel.AutoPayment;

                    if (exitsingPayment != null)
                    {
                        await _paymentService.UpdateAsync(payment);
                    }
                    else
                    {
                        await _paymentService.CreateAsync(payment);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Не удалось получить пациента");
                }
            }

            IEnumerable<Tariff> tariffs = await _tariffService.GetAllAsync();
            IEnumerable<PaymentMethod> methods = await _paymentMethodService.GetAllAsync();

            viewModel.PaymentMethodListItems = from method in methods
                select
                    new SelectListItem()
                    {
                        Text = method.Name,
                        Value = method.Id.ToString(),
                        Selected = (viewModel.SelectedMethodId == method.Id)
                    };
            viewModel.TariffListItems = from tariff in tariffs
                select
                    new SelectListItem
                    {
                        Text = tariff.Name,
                        Value = tariff.Id.ToString(),
                        Selected = (viewModel.SelectedTariffId == tariff.Id)
                    };
            viewModel.PaymentHistoryViewModel = await GetPaymentHistory();

            return View(viewModel);
        }

        private async Task<IEnumerable<PaymentHistoryViewModel>> GetPaymentHistory()
        {
            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            if (patient != null)
                return
                    _paymentHistoryService.GetByPatientIdAsync(patient.Id)
                        .Select(
                            item =>
                                new PaymentHistoryViewModel()
                                {
                                    Date = item.Date,
                                    Value = item.Value,
                                    PaymentType = item.PaymentType
                                });
            else return new List<PaymentHistoryViewModel>();
        }

        [HttpPost]
        public async Task<ActionResult> RefillBalance(PaymentSettingsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
                if (patient != null)
                {
                    
                
                patient.Balance += viewModel.Balance;
                await _patientService.UpdateAsync(patient);

                PaymentHistory paymentHistory = new PaymentHistory();
                paymentHistory.Date = DateTime.Now;
                paymentHistory.Patient = patient;
                paymentHistory.Value = viewModel.Balance;
                paymentHistory.PaymentType = PaymentType.Replenishment;

                await _paymentHistoryService.CreateAsync(paymentHistory);
                }
                else
                {
                    ModelState.AddModelError("","Не удалось получить пациента");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetPaymentMethods()
        {
            IEnumerable<PaymentMethod> methods =   _paymentMethodService.GetAll();
            Core.Models.Patient patient =   _patientService.GetByUserId(User.Identity.GetUserId());
            var paymant =   _paymentService.GetByPatientId(patient?.Id ?? -1);

            PaymentMethodViewModel viewModel = new PaymentMethodViewModel();
            viewModel.CurrentStatusDisplayName = paymant?.PaymentMethod?.Name;
            viewModel.AvailableMethods = from method in methods
                select new PaymentMethodItemViewModel() {Id = method.Id, Name = method.Name};

            return PartialView("_MethodReplenishment", viewModel);
        }


        public async Task<ActionResult> SetPaymentMethods(int id)
        { 
            PaymentMethod method = await _paymentMethodService.GetByIdAsync(id);
            Core.Models.Patient patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            var paymant = await _paymentService.GetByPatientIdAsync(patient?.Id ?? -1);
            paymant.PaymentMethod = method;
            await _paymentService.UpdateAsync(paymant);


            IEnumerable<PaymentMethod> methods = await _paymentMethodService.GetAllAsync();
            PaymentMethodViewModel viewModel = new PaymentMethodViewModel();
            viewModel.CurrentStatusDisplayName = paymant?.PaymentMethod?.Name;
            viewModel.AvailableMethods = from pmethod in methods
                                         select new PaymentMethodItemViewModel() { Id = pmethod.Id, Name = pmethod.Name };

            return PartialView("_MethodReplenishment", viewModel);
        }
    }
}