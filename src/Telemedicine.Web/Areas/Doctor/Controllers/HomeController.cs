using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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

        private readonly IDoctorTimetableService _doctorTimetableService;


        public HomeController(IAppointmentEventService appointmentEventService,
            ITimeSpanEventService timeSpanEventService, IDoctorService doctorService,
            IDoctorTimetableService doctorTimetableService)
        {
            _appointmentEventService = appointmentEventService;
            _timeSpanEventService = timeSpanEventService;
            _doctorService = doctorService;
            _doctorTimetableService = doctorTimetableService;
        }

        public async Task<ActionResult> Index()
        {
            var currentDoctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            IEnumerable<AppointmentEvent> appointments =
                await _appointmentEventService.GetDoctorAppointmentsByDateAsync(currentDoctor.Id, DateTime.Now);
            IEnumerable<AppointmentViewModel> appointmentViewModels = appointments.Where(
                item => item.Status != AppointmentStatus.Declined).OrderBy(a => a.Date).Select(
                    i => new AppointmentViewModel
                    {
                        Date = i.Date,
                        PatientFio = i.Patient.User.DisplayName,
                        Id = i.Id
                    });
            ViewBag.DoctorId = currentDoctor.Id;
            return View(appointmentViewModels);
        }


        public ActionResult AddCalendarEvent()
        {
            return PartialView("_CreateDoctorCalendarEventDialog");
        }

        [HttpPost]
        public async Task<ActionResult> AddCalendarEvent(AddCalendarEventViewModel viewModel)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                TimeSpanEvent timeSpanEvent = new TimeSpanEvent()
                {
                    BeginDate = viewModel.Date,
                    EndDate = viewModel.Date.AddMinutes(viewModel.Duration),
                    IsRepeat = false,
                    Owner = doctor.User,
                };

                await _timeSpanEventService.CreateAsync(timeSpanEvent);
            }
            return PartialView("_CreateDoctorCalendarEventDialog");
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
                TimeSpanEvent timeSpanEvent = new TimeSpanEvent
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

        [HttpGet]
        public async Task<ActionResult> Decline(int id)
        {
            AppointmentEvent appointmentEvent = await _appointmentEventService.GetByIdAsync(id);
            appointmentEvent.Status = AppointmentStatus.Declined;
            await _appointmentEventService.UpdateAsync(appointmentEvent);
            return RedirectToAction(HttpContext?.Request?.UrlReferrer?.Segments.LastOrDefault() == "MyEvents" ? "MyEvents":"Index");
        }


        [HttpGet]
        public async Task<ActionResult> PatientListOnCurrentDay()
        {
            var currentDoctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            IEnumerable<AppointmentEvent> appointments =
                await _appointmentEventService.GetDoctorAppointmentsByDateAsync(currentDoctor.Id, DateTime.Now);
            IEnumerable<AppointmentViewModel> appointmentViewModels = appointments.Where(
                item => item.Status != AppointmentStatus.Declined).OrderBy(a => a.Date).Select(
                    i => new AppointmentViewModel
                    {
                        Date = i.Date,
                        PatientFio = i.Patient.User.DisplayName,
                        Id = i.Id
                    });
            return PartialView("_PatientListOnCurrentDay", appointmentViewModels);
        }

        public async Task<ActionResult> Balance()
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            ViewBag.DoctorId = doctor.Id;
            return View();
        }

        public async Task<string> GetCalendarMarkers(int year, int month)
        {
            var currentDoctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            var dates =
                (await _doctorTimetableService.GetDoctorTimetableByMonthAsync(currentDoctor.Id, year, month)).Where(
                    item => item.AppointmentEvents.Count(eItem => eItem.Status != AppointmentStatus.Declined) != 0);

            var ddw = dates.Select(item => new {Date = item.DateTime.ToString("yyyy-MM-dd"), Class = "planned"});

            return JsonConvert.SerializeObject(ddw);
        }
    }
}