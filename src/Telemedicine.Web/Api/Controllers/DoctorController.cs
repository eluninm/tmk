using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Domain.Models;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Api.Dto;
using Telemedicine.Web.Areas.Doctor.Models.Timetable;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/doctor")]
    [System.Web.Http.Authorize]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentEventService _appointmentService;
        private readonly ITimeSpanEventService _timeSpanService;
        private readonly IDoctorPaymentService _doctorPaymentService;
        private readonly IDoctorTimetableService _doctorTimetableService;

        public DoctorController(
            IDoctorService doctorService,
            IAppointmentEventService appointmentService,
            ITimeSpanEventService timeSpanService,
            IDoctorPaymentService doctorPaymentService, IDoctorTimetableService doctorTimetableService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _timeSpanService = timeSpanService;
            _doctorPaymentService = doctorPaymentService;
            _doctorTimetableService = doctorTimetableService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDoctors(int page = 1, int pageSize = 50, string titleFilter = null,
            int specializationIdFilter = 0)
        {
            try
            {
                var doctors = await _doctorService.PagedAsync(page, pageSize, titleFilter, specializationIdFilter);
                var pagedList = doctors.Map(t =>
                {
                    var doctorDto = Mapper.Map<DoctorListItemDto>(t);
                    doctorDto.StatusText = doctorDto.StatusName == DoctorStatusNames.Busy ? "занят" : "доступен";
                    doctorDto.IsChatAvailable = t.DoctorStatus.Name != DoctorStatusNames.Busy;
                    doctorDto.IsAudioAvailable = t.DoctorStatus.Name == DoctorStatusNames.VideoChat;
                    doctorDto.IsVideoAvailable = t.DoctorStatus.Name == DoctorStatusNames.VideoChat;
                    return doctorDto;
                });
                return Ok(pagedList);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IHttpActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.Description);
        }

        [HttpGet]
        [Route("{id}/appointments")]
        public async Task<IHttpActionResult> Appointments(int id, int page = 1, int pageSize = 10,
            string patientTitleFilter = null)
        {
            var doctorAppointments =
                await _appointmentService.GetDoctorAppointmentsPagedAsync(id, page, pageSize, patientTitleFilter);
            var pagedList = doctorAppointments.Map(t =>
            {
                var doctorAppointmentDto = Mapper.Map<DoctorAppointmentDto>(t);
                doctorAppointmentDto.PatientAvatarUrl = "/img/no_avatar.png";
                return doctorAppointmentDto;
            });

            return Ok(pagedList);
        }

        [HttpGet]
        [Route("{id}/appointments/{date}")]
        public async Task<IHttpActionResult> Appointments(int id, DateTime date)
        {
            var doctorAppointments = await _appointmentService.GetDoctorAppointmentsByDateAsync(id, date);
            return Ok(doctorAppointments);
        }

        [HttpGet]
        [Route("{id}/timeWindows")]
        public async Task<IHttpActionResult> TimeWindows(int id)
        {
            var doctorTimeWindows = await _timeSpanService.GetDoctorTimeWindowsAsync(id);
            return Ok(doctorTimeWindows);
        }

        [HttpGet]
        [Route("{doctorId}/paymentHistory")]
        public async Task<IHttpActionResult> PaymentPage(int doctorId, int page = 1, int pageSize = 10)
        {
            var doctor = await _doctorService.GetByIdAsync(doctorId);

            if (doctor == null)
            {
                return NotFound();
            }
            var payments = await _doctorPaymentService.PagedAsync(doctorId, page, pageSize);
            // var payments = await _paymentHistoryService.PagedAsync(User.Identity.GetUserId(), page, pageSize);
            var pagedList = payments.Map(t =>
            {
                var doctorDto = Mapper.Map<PaymentHistoryDto>(t);
                return doctorDto;
            });

            return Ok(pagedList);
        }

        [HttpGet]
        [Route("{doctorId}/timeline/{year}/{month}")]
        public async Task<IHttpActionResult> GetDoctorTimetableByMonth(int doctorId, int year, int month)
        {
            var timetableViewModel = new List<TimelineDateViewModel>();
            var timetable = await _doctorTimetableService.GetDoctorTimetableByMonthAsync(doctorId, year, month);

            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                List<DoctorTimetable> listDoctorTimetablesOnCurrentDay =
                    timetable.Where(item => item.DateTime.Day == day).ToList();

                //Создаем день Timeline
                TimelineDateViewModel dayOnTimeLine = new TimelineDateViewModel()
                {
                    Date = new DateTime(year, month, day),
                    Hours = new List<TimelineHourViewModel>()
                };

                for (int hour = 0; hour < 24; hour++)
                {
                    //Берем DoctorTimetable на текущий час
                    DoctorTimetable onCurrentHour =
                        listDoctorTimetablesOnCurrentDay.FirstOrDefault(item => item.DateTime.Hour == hour);

                    if (onCurrentHour == null)
                    {
                        TimelineHourViewModel emptyHour = new TimelineHourViewModel()
                        {
                            Hour = hour,
                            HourType = DoctorTimetableHourType.Clear,
                            PatientsCount = 0
                        };
                        dayOnTimeLine.Hours.Add(emptyHour);
                    }
                    else
                    {
                        TimelineHourViewModel notEmptyHour = new TimelineHourViewModel()
                        {
                            Hour = hour,
                            HourType = onCurrentHour.HourType,
                            PatientsCount = onCurrentHour?.AppointmentEvents?.Count(item => item.Status != AppointmentStatus.Declined)??0
                        };
                        dayOnTimeLine.Hours.Add(notEmptyHour);
                    }
                }
                timetableViewModel.Add(dayOnTimeLine);
            }

            return Ok(timetableViewModel);
        }

        private List<TimelineHourViewModel> GetEmptyHours()
        {
            List<TimelineHourViewModel> hour = new List<TimelineHourViewModel>();
            for (int i = 1; i < 25; i++)
            {
                hour.Add(new TimelineHourViewModel()
                {
                    Hour = i,
                    HourType = DoctorTimetableHourType.Clear,
                    PatientsCount = 0
                });
            }

            return hour;
        }


        [HttpPost]
        [Route("{isAvailable}/changeStatus")]
        public async Task<IHttpActionResult> ChangeStatus(bool isAvailable)
        {
            var currentUserId = User.Identity.GetUserId();

            DoctorStatus status = null;

            if (isAvailable)
            {
                status = await _doctorService.GetStatusByNameAsync("Available");
            }
            else
            {
                status = await _doctorService.GetStatusByNameAsync("Busy");
            }

            status = _doctorService.SetStatusByUserId(currentUserId, status.Id);

            var doctor = await _doctorService.GetByUserIdAsync(currentUserId);
            var doctorDto = Mapper.Map<DoctorListItemDto>(doctor);
            doctorDto.StatusText = doctorDto.StatusName == DoctorStatusNames.Busy ? "занят" : "доступен";
            doctorDto.IsChatAvailable = doctor.DoctorStatus.Name != DoctorStatusNames.Busy;
            doctorDto.IsAudioAvailable = doctor.DoctorStatus.Name == DoctorStatusNames.VideoChat;
            doctorDto.IsVideoAvailable = doctor.DoctorStatus.Name == DoctorStatusNames.VideoChat;


            GlobalHost.ConnectionManager.GetHubContext<SignalHub>()
                .Clients.All.OnDoctorUpdated(doctorDto);

            return Ok(true);
        }


        [HttpPost]
        [Route("changeHourStatus/{year}/{month}/{day}/{selectedHour}/{status}")]
        public async Task<IHttpActionResult> ChangeHourStatus(int year, int month, int day, int selectedHour,
            DoctorTimetableHourType status)
        {
            selectedHour = selectedHour == 24 ? 0 : selectedHour;
            var currentUserId = User.Identity.GetUserId();
            var doctor = await _doctorService.GetByUserIdAsync(currentUserId);
            var timetable =
                await
                    _doctorTimetableService.GetTimetableByDate(doctor.Id,
                        new DateTime(year, month, day, selectedHour, 0, 0));
            if (timetable == null)
            {
                timetable = new DoctorTimetable()
                {
                    DateTime = new DateTime(year, month, day, selectedHour, 0, 0),
                    DoctorId = doctor.Id,
                    HourType = status
                };

                await _doctorTimetableService.CreateAsync(timetable);
            }
            else
            {
                if (status == DoctorTimetableHourType.Clear)
                {
                    if (timetable.AppointmentEvents.Count(item => item.Status != AppointmentStatus.Declined) == 0)
                    {
                        timetable.HourType = DoctorTimetableHourType.Clear; 
                    }
                    foreach (AppointmentEvent appointment in timetable.AppointmentEvents)
                    {
                        appointment.Status = AppointmentStatus.Declined;
                    } 
                }
                else
                {
                    timetable.HourType = status; 
                }

                await _doctorTimetableService.UpdateAsync(timetable);
            }

            return Ok();
        }
    }
}