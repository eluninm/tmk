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

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/balance")]
    public class BalanceController : ApiController
    {
        private readonly IDoctorService _doctorService;

        public BalanceController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [Route("debit/{amount}")]
        public async Task<IHttpActionResult> Debit(double amount)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            doctor.Balance -= amount;
            await _doctorService.UpdateAsync(doctor);
            return Ok();
        }
    }
}
