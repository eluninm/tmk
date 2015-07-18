using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/patient")]
    public class PatientController : ApiController
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IConversationService _conversationService;
        private readonly IPatientService _patientService;

        public PatientController(IRecommendationService recommendationService, IConversationService conversationService, IPatientService patientService)
        {
            _recommendationService = recommendationService;
            _conversationService = conversationService;
            _patientService = patientService;
        }

        [HttpGet]
        [Route("{patientId}/recommendations")]
        public async Task<IHttpActionResult> PatientRecommendations(int patientId)
        {
            var recommendations = await _recommendationService.GetPatientRecommendations(patientId);
            return Ok(recommendations.Select(Mapper.Map<RecommendationDto>));
        }

        [HttpGet]
        [Route("{patientId}/consultations")]
        public async Task<IHttpActionResult> PatientConsultations(int patientId)
        {
            var patient = await _patientService.GetByIdAsync(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            var conversations = await _conversationService.GetUserConversations(patient.UserId);
            return Ok(conversations.Select(Mapper.Map<ConsultationDto>));
        }
    }
}
