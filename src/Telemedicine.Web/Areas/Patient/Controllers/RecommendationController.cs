using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = UserRoleNames.Patient)]
    public class RecommendationController : Controller
    {

        private readonly IRecommendationService _recommendationService;
        private readonly IPatientService _patientService;

        public RecommendationController(IRecommendationService recommendationService, IPatientService patientService)
        {
            _recommendationService = recommendationService;
            _patientService = patientService;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> GetRecommendations()
        {
            var patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId());
            IEnumerable<Recommendation> events  = (await _recommendationService.GetAllAsync()).Where(item => item.PatientId == patient.Id && item.IsMarkedAsDeleted == false);
            
            string serialize = JsonConvert.SerializeObject(events);
            return serialize;
        }
    }
}