using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Extensions;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [System.Web.Http.Authorize(Roles = UserRoleNames.Patient + "," + UserRoleNames.Doctor)]
    [RoutePrefix("api/v1/consultation")]
    public class ConsultationController : ApiController
    {
        private readonly IConversationService _conversationService;
        private readonly IPatientService _patientService;

        public ConsultationController(IConversationService conversationService, IPatientService patientService)
        {
            _conversationService = conversationService;
            _patientService = patientService;
        }

        [HttpPost]
        [System.Web.Http.Authorize(Roles = UserRoleNames.Patient)]
        [Route("begin/doctor/{doctorId}")]
        public async Task<IHttpActionResult> BeginConsultation(int doctorId)
        {
            var patient = await _patientService.GetByUserIdAsync(HttpContext.Current.User.Identity.GetUserId());
            if (patient == null)
            {
                return NotFound();
            }

            var consultation = await _conversationService.BeginConsultation(patient.Id, doctorId);
            return Ok(Mapper.Map<ConsultationDto>(consultation));
        }

        [HttpPost]
        [System.Web.Http.Authorize(Roles = UserRoleNames.Doctor)]

        [HttpGet]
        [Route("{conversationId}/messages")]
        public async Task<IHttpActionResult> GetConsultationMessages(string conversationId)
        {
            var currentUserId = RequestContext.Principal.Identity.GetUserId();
            var consultationMessages = await _conversationService.GetConversationMessages(conversationId);
            var dtos = consultationMessages.Select(Mapper.Map<ConsultationMessageDto>).ToList();

            foreach (var d in dtos)
            {
                d.Direction = currentUserId == d.CreatorId ? "me" : "target";
                d.AvatarUrl = "/img/no_avatar.png";
            }

            return Ok(dtos);
        }
    }
}
