using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            var doctorDtos = doctors.Select(t =>
            {
                var doctorDto = Mapper.Map<DoctorListItemDto>(t);
                doctorDto.IsChatAvailable = t.DoctorStatus.Name != DoctorStatusNames.Busy;
                doctorDto.IsAudioAvailable = t.DoctorStatus.Name == DoctorStatusNames.VideoChat;
                doctorDto.IsVideoAvailable = t.DoctorStatus.Name == DoctorStatusNames.VideoChat;
                return doctorDto;
            });

            return Ok(doctorDtos);
        }

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IHttpActionResult> DoctorDetails(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.Description);
        }
    }
}
