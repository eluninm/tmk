using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models;
using Telemedicine.Web.Areas.Doctor.Models;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    [Authorize(Roles = UserRoleNames.Doctor)]
    public class RecommendationsController : Controller
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public RecommendationsController(IDoctorService doctorService, IRecommendationService recommendationService,
            IPatientService patientService)
        {
            _doctorService = doctorService;
            _recommendationService = recommendationService;
            _patientService = patientService;
        } 


        private List<RecommendationViewModel2> GetRecommendations(string id)
        {
            var patient = _patientService.GetByUserId(id);

            var recommendations = _recommendationService.GetAll().ToList();
            recommendations = recommendations.Where(item => item.PatientId == patient.Id).ToList();

            List<RecommendationViewModel2> recommendationViewModel = new List<RecommendationViewModel2>();

            foreach (Recommendation recommendation in recommendations)
            {
                var doctor = _doctorService.GetById(recommendation.DoctorId);
                RecommendationViewModel2 model2 = new RecommendationViewModel2()
                {
                    Content = recommendation.RecommendationText,
                    Date = recommendation.CreateDate,
                    DoctorFIO = doctor?.User?.DisplayName,
                    DoctorSpecialization = doctor?.Specialization?.DisplayName
                };
                recommendationViewModel.Add(model2);
            }

            return recommendationViewModel;
        }

        [HttpPost]
        public async Task<string> GetRecommendations()
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            var patients = await _patientService.GetAllAsync();

            IEnumerable<Recommendation> events;
            if (doctor != null && patients.FirstOrDefault() != null)
                events = (await _recommendationService.GetAllAsync()).Where(item => item.DoctorId == doctor?.Id);
            else
            {
                events = new List<Recommendation>();
            }
            string serialize = JsonConvert.SerializeObject(events);
            return serialize;
        }

 

        public async Task<string> MarkAsDeleted(int recommendaionId)
        {
            if (ModelState.IsValid)
            {
                Recommendation recommendation = await _recommendationService.GetByIdAsync(recommendaionId);
                recommendation.IsMarkedAsDeleted = true;
                await _recommendationService.UpdateAsync(recommendation);
                return JsonConvert.SerializeObject(HttpStatusCode.OK);
            }
            return JsonConvert.SerializeObject(HttpStatusCode.InternalServerError);
        }
    }
}