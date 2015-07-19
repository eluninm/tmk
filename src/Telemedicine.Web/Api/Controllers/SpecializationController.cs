using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/specialization")]
    public class SpecializationController : ApiController
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetSpecializations()
        {
            var specializations = await _specializationService.GetSpecializationsAsync();
            var specializationDtos = specializations.Select(t => Mapper.Map<SpecializationDto>(t)).ToList();
            return Ok(specializationDtos);
        }
    }
}
