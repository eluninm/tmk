using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/balance")]
    public class BalanceController : ApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IPaymentHistoryService _paymentHistoryService;
        public BalanceController(IDoctorService doctorService, IPaymentHistoryService paymentHistoryService, IPatientService patientService)
        {
            _doctorService = doctorService;
            _paymentHistoryService = paymentHistoryService;
            _patientService = patientService;
        }

        [HttpGet]
        [Route("get/")]
        public async Task<IHttpActionResult> GetBalance()
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            return Ok(doctor?.Balance ?? patient?.Balance); 
        }

        [HttpPost]
        [Route("debit/{amount}")]
        public async Task<IHttpActionResult> Debit(double amount)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            if (doctor == null)
            {
                return NotFound();
            }

            await _doctorService.Debit(doctor.Id, amount);
            return Ok();
        }

        [HttpPost]
        [Route("replenish/{amount}")]
        public async Task<IHttpActionResult> Replenish(double amount)
        {
            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            await _patientService.Replenish(patient.Id, amount);
            return Ok();
        }
    }
}
