using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;
using AutoMapper;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/doctor")]
    [Authorize]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentEventService _appointmentService;

        public DoctorController(IDoctorService doctorService, IAppointmentEventService appointmentService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDoctors(int page = 1, int pageSize = 50, string titleFilter = null, int specializationIdFilter = 0)
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
            catch (Exception ex)
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
        public async Task<IHttpActionResult> Appointments(int id, int page = 1, int pageSize = 10, string patientTitleFiter = null)
        {
            var doctorAppointments = await _appointmentService.GetDoctorAppointmentsPagedAsync(id, page, pageSize, patientTitleFiter);
            var pagedList = doctorAppointments.Map(t =>
            {
                var doctorAppointmentDto = Mapper.Map<DoctorAppointmentDto>(t);
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
    }
}
