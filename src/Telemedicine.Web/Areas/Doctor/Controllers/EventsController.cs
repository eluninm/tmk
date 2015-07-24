using System;
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
    public class EventsController : Controller
    {
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public EventsController(IAppointmentEventService appointmentEventService,
            ITimeSpanEventService timeSpanEventService, IDoctorService doctorService, IPatientService patientService)
        {
            _appointmentEventService = appointmentEventService;
            _timeSpanEventService = timeSpanEventService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public async Task<string> GetEvents()
        {
            
            List<EventViewModel> eventList = new List<EventViewModel>();

            //eventList.AddRange(
            //    (await _appointmentEventService.GetAllAsync()).Select(
            //        ae => new EventViewModel() {start = ae.Date, title = "Прием пациента"}));
             
            var timeSpanEvents = (await _timeSpanEventService.GetAllAsync());

            foreach (TimeSpanEvent timeSpanEvent in timeSpanEvents)
            {
                int id = (new Random()).Next(0, 999999);
                List<DayOfWeek> days = GetDaysOfWeek(timeSpanEvent);

                if (days.Any())
                { 
                    List<DateTime> dates = new List<DateTime>();
                    dates.Add(timeSpanEvent.BeginDate);
                    foreach (DayOfWeek dayOfWeek in days)
                    {
                        dates.AddRange(GetDate(dayOfWeek, timeSpanEvent));
                        eventList.AddRange(dates.Select(date => new EventViewModel()
                        { 
                            start = new DateTime(date.Year, date.Month, date.Day, timeSpanEvent.BeginDate.Hour, timeSpanEvent.BeginDate.Minute, 0),
                            end = timeSpanEvent.EndDate == null ? (DateTime?) null : (new DateTime(date.Year, date.Month, date.Day, timeSpanEvent.EndDate?.Hour ?? 0,
                            timeSpanEvent.EndDate?.Minute ?? 0, 0)),
                            title = timeSpanEvent.Title,
                            allDay = timeSpanEvent.IsRepeat
                        }));
                        dates.Clear();
                    }
                }
                else
                {
                    eventList.Add(new EventViewModel()
                    {
                        start = timeSpanEvent.BeginDate,
                        end = timeSpanEvent.EndDate,
                        title = timeSpanEvent.Title,
                        allDay = timeSpanEvent.IsRepeat
                    });
                }
            }

            return JsonConvert.SerializeObject(eventList);
        }


        private List<DateTime> GetDate(DayOfWeek dayOfWeek, TimeSpanEvent timeSpanEvent)
        {
            List<DateTime> list = new List<DateTime>(); 
            DateTime beginDate = timeSpanEvent.BeginDate.AddDays(1);
            DateTime endDate = new DateTime(beginDate.Year + 1, 1, 1);

            while (beginDate.DayOfWeek != dayOfWeek)
            {
                beginDate = beginDate.AddDays(1);
            }

            while (beginDate <= endDate)
            {
                if (beginDate.DayOfWeek == dayOfWeek)
                {
                    list.Add(beginDate);
                }

                beginDate = beginDate.AddDays(7);
            }

            return list;
        }

        private List<DayOfWeek> GetDaysOfWeek(TimeSpanEvent timeSpanEvent)
        {
            List<DayOfWeek> list = new List<DayOfWeek>();
            if (timeSpanEvent.Monday)
            {
                list.Add(DayOfWeek.Monday);
            }

            if (timeSpanEvent.Tuesday)
            {
                list.Add(DayOfWeek.Tuesday);
            }

            if (timeSpanEvent.Wednesday)
            {
                list.Add(DayOfWeek.Wednesday);
            }

            if (timeSpanEvent.Thursday)
            {
                list.Add(DayOfWeek.Thursday);
            }

            if (timeSpanEvent.Friday)
            {
                list.Add(DayOfWeek.Friday);
            }

            if (timeSpanEvent.Saturday)
            {
                list.Add(DayOfWeek.Saturday);
            }

            if (timeSpanEvent.Sunday)
            {
                list.Add(DayOfWeek.Sunday);
            } 

            return list;
        }
    }
}