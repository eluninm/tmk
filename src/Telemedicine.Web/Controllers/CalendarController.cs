using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Areas.Doctor.Models;
using Telemedicine.Web.Areas.Patient.Models;

namespace Telemedicine.Web.Controllers
{
    [Authorize(Roles = UserRoleNames.Doctor + "," + UserRoleNames.Patient)]
    public class CalendarController : Controller
    {
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IUserEventsService _eventsService;
        private readonly SiteUserManager _siteUserManager;
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly IPatientService _patientService;

        public CalendarController(ITimeSpanEventService timeSpanEventService, IUserEventsService eventsService,
            SiteUserManager siteUserManager, IAppointmentEventService appointmentEventService,
            IPatientService patientService)
        {
            _timeSpanEventService = timeSpanEventService;
            _eventsService = eventsService;
            _siteUserManager = siteUserManager;
            _appointmentEventService = appointmentEventService;
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult> AddCalendarEvent(UserEventViewModel userEventViewModel)
        {
            UserEvent userEvent = new UserEvent()
            {
                Content = userEventViewModel.Comments,
                Creator = _siteUserManager.FindById(User.Identity.GetUserId())
            };
            await _eventsService.CreateAsync(userEvent);

            /*TimeSpanEvent timeSpanEvent = new TimeSpanEvent()
            {
                BeginDate = UserEventViewModel.Date,
                Topic = UserEventViewModel.Topic,
                Comments = UserEventViewModel.Comments,
                OwnerUserId = User.Identity.GetUserId(),
                UserEvent = userEvent,
                EventType = EventType.Calendar
            }; 

            await _timeSpanEventService.CreateAsync(timeSpanEvent);*/

            return PartialView("Calendar");
        }

        public async Task<string> GetCalendarEvents()
        {
            IEnumerable<AddAppointmentEventViewModel> events =
                (await _appointmentEventService.GetAllAsync()).Where(
                    item => item.Patient.User.Id == User.Identity.GetUserId())
                    .Select(item => new AddAppointmentEventViewModel()
                    {Date = item.Date, Topic = "Запись к врачу", DoctorFIO = item.Doctor.User.DisplayName}).ToList();
            events = events.OrderByDescending(item => item.Date);
            string serialize = JsonConvert.SerializeObject(events);
            return serialize;
        }
    }
}