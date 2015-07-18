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

        public ActionResult List()
        {
            return View();
        }


        public ActionResult AddRecommendationStartDialog(string id)
        {
            RecommendationViewModel model = new RecommendationViewModel()
            {
                PatientUserId = id
            };

            return PartialView("_AddRecommendationDialog", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddRecommendation(RecommendationViewModel viewModel)
        {
            var doctor = await _doctorService.GetByUserIdAsync(User.Identity.GetUserId());
            var patient = await _patientService.GetByUserIdAsync(viewModel.PatientUserId);
            if (ModelState.IsValid)
            {
                Recommendation recommendation = new Recommendation()
                {
                    CreateDate = DateTime.Now,
                    RecommendationText = viewModel.RecommendationText,
                    DoctorId = doctor.Id,
                    PatientId = patient.Id
                };

                await _recommendationService.CreateAsync(recommendation);
                return new EmptyResult();
            }
            return PartialView("_AddRecommendationDialog", viewModel);
        }

        public ActionResult List2(string id)
        {
            var recommendations = GetRecommendations(id);
            return PartialView(recommendations);
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


        [HttpPost]
        public async Task<ActionResult> CreateRecommendation(RecommendationViewModel recommendation)
        {
            if (ModelState.IsValid)
            {
                var doctor = _doctorService.GetByUserIdAsync(User.Identity.GetUserId());

                return PartialView("Recommendations");
            }

            return new HttpStatusCodeResult(400);
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