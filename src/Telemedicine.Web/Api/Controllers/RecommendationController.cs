using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/recommendation")]
    public class RecommendationController : ApiController
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public RecommendationController(IRecommendationService recommendationService, IDoctorService doctorService,
            IPatientService patientService)
        {
            _recommendationService = recommendationService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [System.Web.Http.Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var recommendation = await _recommendationService.GetByIdAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RecommendationDto>(recommendation));
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("addRecommendation/{patientId}/{recommendationText}/")]
        public async Task<IHttpActionResult> AddRecommendation(int patientId, string recommendationText)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            var patient = await _patientService.GetByIdAsync(patientId);

            Recommendation recommendation = new Recommendation()
            {
                CreateDate = DateTime.Now,
                RecommendationText = recommendationText,
                DoctorId = doctor.Id,
                PatientId = patient.Id
            };

            await _recommendationService.CreateAsync(recommendation);
            return Ok();
        }
    }
}