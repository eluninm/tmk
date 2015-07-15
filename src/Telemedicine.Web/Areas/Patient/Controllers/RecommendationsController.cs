using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Web.Areas.Doctor.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public RecommendationsController(IPatientService patientService, IDoctorService doctorService, IRecommendationService recommendationService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _recommendationService = recommendationService;
        }
 
        public ActionResult List2()
        { 
            List<RecommendationViewModel2> task = GetRecommendations();
            task = task.OrderByDescending(item => item.Date).ToList();
            return PartialView(task);
        }

        private List<RecommendationViewModel2> GetRecommendations()
        {
            var patient = _patientService.GetByUserId(User.Identity.GetUserId());

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
    }
}