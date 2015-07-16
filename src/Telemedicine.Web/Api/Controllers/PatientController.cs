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
    [RoutePrefix("api/v1/patients")]
    public class PatientController : ApiController
    {
        private readonly IRecommendationService _recommendationService;

        public PatientController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet]
        [Route("{patientId}/recommendations")]
        public async Task<IHttpActionResult> PatientRecommendations(int patientId)
        {
            var recommendations = await _recommendationService.GetPatientRecommendations(patientId);
            return Ok(recommendations.Select(Mapper.Map<RecommendationDto>));
        }
    }
}
