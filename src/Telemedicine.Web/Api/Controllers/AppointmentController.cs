using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Api.Dto;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/appointment")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IDoctorTimetableService _doctorTimetableService;

        public AppointmentController(IAppointmentEventService appointmentEventService, IDoctorService doctorService,
            IPatientService patientService, IDoctorTimetableService doctorTimetableService)
        {
            _appointmentEventService = appointmentEventService;
            _doctorService = doctorService;
            _patientService = patientService;
            _doctorTimetableService = doctorTimetableService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddAppointment(AppointmentDto dto)
        {
            var currentPatient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                AppointmentEvent appointmentEvent = new AppointmentEvent()
                {
                    Date = dto.AppointmentDate,
                    Doctor = await _doctorService.GetByIdAsync(dto.DoctorId),
                    Patient = currentPatient,
                    Created = DateTime.Now ,
                    Status = AppointmentStatus.Ready
                };
                try
                {
                //await _appointmentEventService.CreateAsync(appointmentEvent);

                    DoctorTimetable timetable =
                       await _doctorTimetableService.GetTimetableByDate(dto.DoctorId, dto.AppointmentDate);
                    if (timetable == null)
                    {
                        timetable = new DoctorTimetable()
                        {
                            DateTime = dto.AppointmentDate,
                            HourType = DoctorTimetableHourType.Working,
                            DoctorId = dto.DoctorId,
                            AppointmentEvents = new List<AppointmentEvent>()
                           
                        };
                        timetable = await _doctorTimetableService.CreateAsync(timetable);
                        timetable.AppointmentEvents.Add(appointmentEvent);
                        timetable = await _doctorTimetableService.UpdateAsync(timetable);
                        Debug.WriteLine("Create");
                    }
                    else
                    {
                        if (timetable.HourType != DoctorTimetableHourType.NotWorking)
                        {
                            timetable.HourType = DoctorTimetableHourType.Working;
                            timetable.AppointmentEvents.Add(appointmentEvent);
                            timetable = await _doctorTimetableService.UpdateAsync(timetable);
                            Debug.WriteLine("Update");
                        } 
                    }

                    GlobalHost.ConnectionManager.GetHubContext<AppointmentHub>()
                        .Clients.Client(
                            SignalHub._connections.GetConnections(appointmentEvent.Doctor.UserId).FirstOrDefault()).OnNewAppointment();
                }
                catch (Exception exp)
                {
                   Debug.WriteLine(exp.Message);
                   Debug.WriteLine(exp.InnerException.Message);
                   Debug.WriteLine(exp.InnerException.Data);
                }

                return Ok();
            }
            return NotFound();
        }
    }
}