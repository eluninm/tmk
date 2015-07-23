using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/appointment")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public AppointmentController(IAppointmentEventService appointmentEventService, IDoctorService doctorService, IPatientService patientService)
        {
            _appointmentEventService = appointmentEventService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddAppointment(int doctorId, DateTime appointmentDate)
        {
            if (ModelState.IsValid)
            {
                AppointmentEvent appointmentEvent = new AppointmentEvent()
                {
                    Date = appointmentDate,
                    Doctor = await _doctorService.GetByIdAsync(doctorId),
                    Patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId()),
                    Created = DateTime.Now
                };
                await _appointmentEventService.CreateAsync(appointmentEvent);
                return Ok();
            }
            return NotFound();
        }
    }
}
