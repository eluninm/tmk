using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Areas.Doctor.Models;
using Telemedicine.Web.Areas.Administrator.Models;
using Telemedicine.Web.Model;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{

    public class _HomeController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly SiteUserManager _siteUserManager;
        private readonly IDoctorService _doctorService;
        private readonly ISpecializationService _specializationService;
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IUserEventsService _userEventsService;


        public _HomeController(IPatientService patientService, SiteUserManager siteUserManager, IDoctorService doctorService,
            ISpecializationService specializationService,
            ITimeSpanEventService timeSpanEventService, IUserEventsService userEventsService)
        {
            _patientService = patientService;
            _siteUserManager = siteUserManager;
            _doctorService = doctorService;
            _specializationService = specializationService;
            _timeSpanEventService = timeSpanEventService;
            _userEventsService = userEventsService;

        }

        public ActionResult MyEvents()
        {
            return View();
        }

        public async Task<JsonResult> GetDoctorList(int SpecID)
        {

            List<JSerialDoctors> JsonResult = new List<JSerialDoctors>();

            var doctors = await _doctorService.GetAllAsync();
            var result = doctors.Where(c => c.SpecializationId == SpecID);

            foreach (var item in result)
            {


                JsonResult.Add(new JSerialDoctors
                {
                    Id = item.Id,
                    SiteUserId = item.UserId,
                    DisplayName = item.User.DisplayName
                });
            }

            var ResultClass = JsonResult;



            return Json(ResultClass, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> GetSpecializationList(int SpecID)
        {
            List<JSerialDoctors> JsonResult = new List<JSerialDoctors>();
            var SpecializationList = await _specializationService.GetSpecializationsAsync();
            var Result = SpecializationList.Select(c => new { c.DisplayName, c.Id }).ToList();
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
    }
}
