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
            /*await _appointmentEventService.CreateAsync(new AppointmentEvent()
            {
                Date = DateTime.Today,
                DateCreation = DateTime.Now,
                Doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId()),
                Patient = await _patientService.GetByIdAsync(2)
            });


            await _timeSpanEventService.CreateAsync(new TimeSpanEvent()
            {
                BeginDate = new DateTime(2015,7,9,1,0,0),
                EndDate = new DateTime(2015,7,9,22,0,0),
                Owner = (await _doctorService.GetByUserIdAsync(User.Identity.GetUserId())).User,
                Title = "День рождения"
            });
            */
            List<EventViewModel> eventList = new List<EventViewModel>();

            eventList.AddRange(
                (await _appointmentEventService.GetAllAsync()).Select(
                    ae => new EventViewModel() {start = ae.Date, title = "Прием пациента", color = "#38cbb5"}));
            /*eventList.AddRange(
                (await _timeSpanEventService.GetAllAsync()).Select(
                    ts =>
                        new EventViewModel()
                        {
                            start = ts.BeginDate,
                            end = ts.EndDate,
                            title = ts.Title,
                            color = "#c0e788",
                            allDay = ts.EndDate == null
                        }));*/
            var timeSpanEvents = (await _timeSpanEventService.GetAllAsync());

            foreach (TimeSpanEvent timeSpanEvent in timeSpanEvents)
            {
                if (timeSpanEvent.IsRepeat)
                {
                    int id = (new Random()).Next(0, 999999);
                    List<DayOfWeek?> days = GetDaysOfWeek(timeSpanEvent);
                    if (timeSpanEvent.RepeatType == TypeRepeatingEvent.Everyday)
                    {
                        days.Clear();
                        days.Add(null);
                    }

                    List<DateTime> dates = new List<DateTime>();
                    dates.Add(timeSpanEvent.BeginDate);
                    foreach (DayOfWeek? dayOfWeek in days)
                    {
                        dates.AddRange(GetDate(dayOfWeek, timeSpanEvent));
                        foreach (DateTime date in dates)
                        {
                            eventList.Add(new EventViewModel()
                            {
                                start =
                                    new DateTime(date.Year, date.Month, date.Day, timeSpanEvent.BeginDate.Hour,
                                        timeSpanEvent.BeginDate.Minute, 0),
                                end =
                                    timeSpanEvent.EndDate == null
                                        ? (DateTime?) null
                                        : (new DateTime(date.Year, date.Month, date.Day,
                                            timeSpanEvent.EndDate?.Hour ?? 0, timeSpanEvent.EndDate?.Minute ?? 0, 0)),
                                title = timeSpanEvent.Title,
                                color = "#c0e788",
                                allDay = false
                            });
                        }
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
                        color = "#c0e788",
                        allDay = timeSpanEvent.EndDate == null
                    });
                }
            }

            return JsonConvert.SerializeObject(eventList);
        }


        private List<DateTime> GetDate(DayOfWeek? dayOfWeek, TimeSpanEvent timeSpanEvent)
        {
            List<DateTime> list = new List<DateTime>();
            DateTime now = DateTime.Now;
            DateTime beginDate = timeSpanEvent.BeginDate;
            DateTime endDate = new DateTime(now.Year + 2, 1, 1);
            if (dayOfWeek != null)
            {
                if (beginDate.DayOfWeek != DayOfWeek.Monday && timeSpanEvent.RepeatType != TypeRepeatingEvent.Everyday)
                    while (beginDate.DayOfWeek != DayOfWeek.Monday)
                    {
                        beginDate = beginDate.Subtract(new TimeSpan(1, 0, 0, 0));
                    }


                while (beginDate.DayOfWeek != dayOfWeek)
                {
                    beginDate = beginDate.AddDays(1);
                }

                if (((timeSpanEvent.RepeatType == TypeRepeatingEvent.Everyday) || (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryBusinessDay ||
                         timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryMondayWednesdayFriday ||
                         timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryTuesdayThursday) || (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryWeek))
                         && beginDate > timeSpanEvent.BeginDate  )
                {
                    list.Add(beginDate);
                }
                 
            }
            int repeatCouner = 0;
            timeSpanEvent.RepeatInterval = (timeSpanEvent.RepeatInterval > 0
                ? timeSpanEvent.RepeatInterval - 1
                : timeSpanEvent.RepeatInterval);

            while (beginDate < endDate &&
                   (((timeSpanEvent.Interval == EndTypeRepeatInterval.AfterDate) && beginDate < timeSpanEvent.EndDate) ||
                    ((timeSpanEvent.Interval == EndTypeRepeatInterval.AfterNRepeat) &&
                     repeatCouner < timeSpanEvent.RepeatCount) ||
                    (timeSpanEvent.Interval == EndTypeRepeatInterval.Never)))
            {
                repeatCouner++;
                if (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryYear)
                {
                    beginDate = beginDate.AddYears(1 + timeSpanEvent.RepeatInterval);
                }
                else if (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryMonth)
                {
                    beginDate = beginDate.AddMonths(1 + timeSpanEvent.RepeatInterval);
                }
                else if (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryWeek)
                {
                    beginDate = beginDate.AddDays(7 *(timeSpanEvent.RepeatInterval==0?1: timeSpanEvent.RepeatInterval) );
                }
                else if (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryBusinessDay ||
                         timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryMondayWednesdayFriday ||
                         timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryTuesdayThursday)
                {
                    beginDate = beginDate.AddDays(7);
                }
                else
                {
                    beginDate =
                        beginDate.AddDays(1 + timeSpanEvent.RepeatInterval );
                }

                list.Add(beginDate);
            }

            return list;
        }

        private List<DayOfWeek?> GetDaysOfWeek(TimeSpanEvent timeSpanEvent)
        {
            List<DayOfWeek?> list = new List<DayOfWeek?>();
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

            if (timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryYear || timeSpanEvent.RepeatType == TypeRepeatingEvent.EveryMonth)
            {
                list.Add(timeSpanEvent.BeginDate.DayOfWeek);
            }

            return list;
        }
    }
}