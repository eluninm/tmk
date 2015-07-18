using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Extensions;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/consultations")]
    public class ConsultationController : ApiController
    {
        private readonly IConversationService _conversationService;

        public ConsultationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        //[HttpPost]
        //public async Task<IHttpActionResult> BeginConsultation(int doctorId)
        //{
        //    _conversationService.BeginConversation2(doctorId);
        //}
            
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
