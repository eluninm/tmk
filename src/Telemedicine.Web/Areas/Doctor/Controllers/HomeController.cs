using System;
using System.Collections;
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
using Telemedicine.Web.Areas.Doctor.Models;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    [Authorize(Roles = UserRoleNames.Doctor)]
    public class HomeController : Controller
    {
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IDoctorService _doctorService;

        public HomeController(IAppointmentEventService appointmentEventService,
            ITimeSpanEventService timeSpanEventService, IDoctorService doctorService)
        {
            _appointmentEventService = appointmentEventService;
            _timeSpanEventService = timeSpanEventService;
            _doctorService = doctorService;
        }

        public async Task<ActionResult> Index()
        {
            var currentDoctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            ViewBag.Balance = currentDoctor.Balance;
            IEnumerable<AppointmentEvent> appointments = await _appointmentEventService.GetDoctorAppointmentsByDateAsync(currentDoctor.Id, DateTime.Now);
            IEnumerable<AppointmentViewModel> appointmentViewModels = appointments.OrderBy(a => a.Date).Select(
                i => new AppointmentViewModel
                {
                    Date = i.Date,
                    PatientFio = i.Patient.User.DisplayName
                });

            return View(appointmentViewModels);
        }

        public ActionResult AddTimeSpanEvent()
        {
            AddEventViewModel viewModel = new AddEventViewModel();
            List<SelectListItem> listItems = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                listItems.Add(new SelectListItem() {Text = i.ToString(), Value = i.ToString()});
            }
             
            viewModel.Begin = DateTime.Now; 
            return PartialView("_AddTimeSpanEventDialog", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddTimeSpanEvent(AddEventViewModel viewModel)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                TimeSpanEvent timeSpanEvent = new TimeSpanEvent()
                {
                    BeginDate = viewModel.Begin,
                    EndDate = viewModel.End,
                    Title = viewModel.Title,
                    Owner = doctor.User,
                    IsRepeat = viewModel.IsOnDate,

                    Monday = viewModel.Monday,
                    Tuesday = viewModel.Tuesday,
                    Wednesday = viewModel.Wednesday,
                    Thursday = viewModel.Thursday,
                    Friday = viewModel.Friday,
                    Saturday = viewModel.Saturday,
                    Sunday = viewModel.Sunday
                };

                 
                await _timeSpanEventService.CreateAsync(timeSpanEvent);
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                listItems.Add(new SelectListItem() {Text = i.ToString(), Value = i.ToString()});
            }
             
            viewModel.Begin = DateTime.Now;
            return PartialView("_AddTimeSpanEventDialog", viewModel);
        }

        public async Task<ActionResult> MyEvents()
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            ViewBag.DoctorId = doctor.Id;
            ViewBag.Balance = (await _doctorService.GetByUserIdAsync(User.Identity.GetUserId())).Balance;
            return View();
        }

        public async Task<ActionResult> Balance()
        {
            ViewBag.Balance = (await _doctorService.GetByUserIdAsync(User.Identity.GetUserId())).Balance;
            return View();
        }
    }
}