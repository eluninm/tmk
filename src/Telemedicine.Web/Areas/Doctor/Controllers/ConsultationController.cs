using System;
using System.Collections;
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

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IDoctorService _doctorService;

        public ConsultationController(IAppointmentEventService appointmentEventService,
            ITimeSpanEventService timeSpanEventService, IDoctorService doctorService)
        {
            _appointmentEventService = appointmentEventService;
            _timeSpanEventService = timeSpanEventService;
            _doctorService = doctorService;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<AppointmentEvent> appointments = await _appointmentEventService.GetAllAsync();
            IEnumerable<AppointmentViewModel> appointmentViewModels =
                appointments.Select(
                    i => new AppointmentViewModel() {Date = i.Date, PatientFio = i.Patient.User.DisplayName});
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

            viewModel.IntervalItems = listItems;
            viewModel.Begin = DateTime.Now;
            viewModel.RepeatBeginDate = DateTime.Now;
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
                    BeginDate = viewModel.IsRepeat ? viewModel.RepeatBeginDate : viewModel.Begin,
                    EndDate = viewModel.IsRepeat ? viewModel.RepeatEndDate : viewModel.End,
                    Title = viewModel.Title,
                    IsRepeat = viewModel.IsRepeat, 
                    Owner = doctor.User,
                    Interval = viewModel.SelectedEndType,
                    RepeatType = viewModel.SelectedRepeatType,
                    RepeatInterval = viewModel.SelectedIntervalId
                };

                timeSpanEvent.RepeatCount = viewModel.After;
                switch (viewModel.SelectedRepeatType)
                {
                    case TypeRepeatingEvent.EveryBusinessDay:
                    {
                        timeSpanEvent.Monday = true;
                        timeSpanEvent.Tuesday = true;
                        timeSpanEvent.Wednesday = true;
                        timeSpanEvent.Thursday = true;
                        timeSpanEvent.Friday = true;
                        timeSpanEvent.Interval = 0;
                        timeSpanEvent.RepeatInterval = 0;
                        break;
                    }

                    case TypeRepeatingEvent.EveryMondayWednesdayFriday:
                    {
                        timeSpanEvent.Monday = true;
                        timeSpanEvent.Wednesday = true;
                        timeSpanEvent.Friday = true;
                            timeSpanEvent.Interval = 0;
                            timeSpanEvent.RepeatInterval = 0;
                            break;
                    }
                    case TypeRepeatingEvent.EveryTuesdayThursday:
                    {
                        timeSpanEvent.Tuesday = true;
                        timeSpanEvent.Thursday = true;
                            timeSpanEvent.Interval = 0;
                            timeSpanEvent.RepeatInterval = 0;
                            break;
                    }
                    case TypeRepeatingEvent.Everyday:
                    {
                        timeSpanEvent.Monday = true;
                        timeSpanEvent.Tuesday = true;
                        timeSpanEvent.Wednesday = true;
                        timeSpanEvent.Thursday = true;
                        timeSpanEvent.Friday = true;
                        timeSpanEvent.Saturday = true;
                        timeSpanEvent.Sunday = true;
                        break;
                    }
                    case TypeRepeatingEvent.EveryWeek:
                        {
                            timeSpanEvent.Monday = viewModel.Monday;
                            timeSpanEvent.Tuesday = viewModel.Tuesday;
                            timeSpanEvent.Wednesday = viewModel.Wednesday;
                            timeSpanEvent.Thursday = viewModel.Thursday;
                            timeSpanEvent.Friday = viewModel.Friday;
                            timeSpanEvent.Saturday = viewModel.Saturday;
                            timeSpanEvent.Sunday = viewModel.Sunday;
                            break;
                        }
                }
                await _timeSpanEventService.CreateAsync(timeSpanEvent);
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                listItems.Add(new SelectListItem() {Text = i.ToString(), Value = i.ToString()});
            }

            viewModel.IntervalItems = listItems;
            viewModel.Begin = DateTime.Now;
            return PartialView("_AddTimeSpanEventDialog", viewModel);
        }
    }
}