using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.PagedList;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/patient")]
    public class PatientController : ApiController
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IConversationService _conversationService;
        private readonly IPatientService _patientService;
        private readonly IPaymentHistoryService _paymentHistoryService;
        private readonly IAppointmentEventService _appointmentService;

        public PatientController(
            IRecommendationService recommendationService, 
            IConversationService conversationService, 
            IPatientService patientService, 
            IPaymentHistoryService paymentHistoryService,
            IAppointmentEventService appointmentService)
        {
            _recommendationService = recommendationService;
            _conversationService = conversationService;
            _patientService = patientService;
            _paymentHistoryService = paymentHistoryService;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("{patientId}/recommendations")]
        public async Task<IHttpActionResult> PatientRecommendations(int patientId)
        {
            var recommendations = await _recommendationService.GetPatientRecommendations(patientId);
            return Ok(recommendations.Select(Mapper.Map<RecommendationDto>));
        }

        [HttpGet]
        [Route("PaymentsHistory/")]
        public async Task<IHttpActionResult> PaymentsHistory()
        {
            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            if (patient == null)
            {
                return NotFound();
            }

            var paymentsHistory = _paymentHistoryService.GetByPatientIdAsync(patient.Id).ToList();

            return Ok(paymentsHistory.Select(Mapper.Map<PaymentHistoryDto>));
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
            return Ok(conversations.Select(Mapper.Map<ConsultationInfoDto>));
        }

        [HttpGet]
        [Route("{patientId}/paymentHistory")]
        public async Task<IHttpActionResult> PaymentPage(int patientId, int page = 1, int pageSize = 10)
        {
            var patient = await _patientService.GetByIdAsync(patientId);

            if (patient == null)
            {
                return NotFound();
            }


            var payments = await _paymentHistoryService.PagedAsync(User.Identity.GetUserId(), page, pageSize);
            var pagedList = payments.Map(t =>
            {
                var doctorDto = Mapper.Map<PaymentHistoryDto>(t);
                return doctorDto;
            });

            return Ok(pagedList);
        }

        [HttpGet]
        [Route("{id}/appointments")]
        public async Task<IHttpActionResult> Appointments(int id, int page = 1, int pageSize = 10)
        {
            var patientAppointments = await _appointmentService.GetPatientAppointmentsPagedAsync(id, page, pageSize);
            var pagedList = patientAppointments.Map(t =>
            {
                var patientAppointmentDto = Mapper.Map<PatientAppointmentDto>(t);
                return patientAppointmentDto;
            });

            return Ok(pagedList);
        }
    }
}
