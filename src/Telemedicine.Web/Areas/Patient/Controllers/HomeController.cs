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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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


        public HomeController(
            IPatientService patientService
            , SiteUserManager userManager
            , IDoctorService doctorService
            , ITimeSpanEventService timeSpanEventService
            , IPaymentService paymentService
            , IUserEventsService userEventsService)
        {
            _patientService = patientService;
            _userManager = userManager;
            _doctorService = doctorService;
            _timeSpanEventService = timeSpanEventService;
            _paymentService = paymentService;
            _userEventsService = userEventsService;
        }

        //public async Task<ActionResult> Index()
        //{
        //    var doctors = await _doctorService.GetAllAsync();
        //    var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());

        //    SelectDoctorViewModel viewModel = new SelectDoctorViewModel();
        //    viewModel.Doctors = new List<DoctorViewModel>();

        //    viewModel.Doctors = (from doctor in doctors select
        //                             new DoctorViewModel()
        //                             {
        //                                 FIO = doctor?.User?.DisplayName,
        //                                 Id = doctor?.Id ?? -1,
        //                                 Specialization = doctor?.Specialization?.DisplayName,
        //                                 Status = doctor?.DoctorStatus?.DisplayName
        //                             }).ToList();

        //    return View(viewModel);
        //}

        public ActionResult MyEvents()
        {
            return View();
        }

        public async Task<ActionResult> Index_()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var patient = await _patientService.GetByUserIdAsync(user.Id);

          /*  if (patient.SelectedDoctorId == 0)
            {
                return RedirectToAction("SelectDoctor");
            }*/

            ViewBag.Balance = patient.Balance;


            ViewBag.PaymentInstrumentEmpty = user.PaymentInstrument;
            ViewBag.FirstLogin = user.LastLoginDate == null;

            var doctors = await _doctorService.GetAllAsync();
            ViewBag.Doctors = JsonConvert.SerializeObject(doctors.ToList());

            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return View();
        }

        public async Task<ActionResult> GetDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SetDoctor(string siteUserId, string date)
        {
            var patientSiteUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var patient = await _patientService.GetByUserIdAsync(patientSiteUser.Id);
            var doctorSiteUser = await _userManager.FindByIdAsync(siteUserId);

           /* try
            {
                var newUserEvent = new Core.Models.UserEvent
                {
                    SiteUser = doctorSiteUser,
                    EventId = "EventId " + Guid.NewGuid().ToString()
                };

                var newCalendarEvent = await _timeSpanEventService.CreateAsync(new Core.Models.TimeSpanEvent
                {
                    BeginDate = DateTime.Parse(date),
                    EndDate = DateTime.Parse(date),
                    //OwnerUserId = siteUserId,                
                    OwnerUser = patientSiteUser,
                    Comments =
                        "Событие от пациента " +
                        string.Format("{0} {1}", patientSiteUser.LastName, patientSiteUser.FirstName),
                    UserEvent = newUserEvent
                });

                newUserEvent.CalendarEvent = newCalendarEvent;
                await _userEventsService.UpdateAsync(newUserEvent);
            }
            catch (DbEntityValidationException e)
            {
                var errorMessage = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errorMessage = string.Format("PropertyName {0} ErrorMessage {1}", ve.PropertyName,
                            ve.ErrorMessage);
                    }
                }
                return Json(errorMessage, JsonRequestBehavior.DenyGet);
            }
            */
            // Вычет денежных средств из баланса
            patient.Balance -= 500;
            await _patientService.UpdateAsync(patient);

            return Json("ok", JsonRequestBehavior.DenyGet);
        }

        public async Task<ActionResult> GetEvents()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var events = await _timeSpanEventService.GetAllAsync();

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetBalance()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var patient = await _patientService.GetByUserIdAsync(user.Id);

            return Json(patient.Balance, JsonRequestBehavior.AllowGet);
        }
    }
}