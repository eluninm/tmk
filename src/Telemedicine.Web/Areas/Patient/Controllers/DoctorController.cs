using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
using Telemedicine.Web.Areas.Patient.Models;

namespace Telemedicine.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = UserRoleNames.Patient)]
    public class DoctorController : Controller
    {
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly ITimeSpanEventService _timeSpanEventService;
        private readonly IUserEventsService _userEventsService;
        private readonly ISpecializationService _specializationService;


        public DoctorController(
            IAppointmentEventService appointmentEventService, 
            IDoctorService doctorService, 
            IPatientService patientService, 
            ITimeSpanEventService timeSpanEventService, 
            IUserEventsService userEventsService,
            ISpecializationService specializationService)
        {
            _appointmentEventService = appointmentEventService;
            _doctorService = doctorService;
            _patientService = patientService;
            _timeSpanEventService = timeSpanEventService;
            _userEventsService = userEventsService;
            _specializationService = specializationService;
        }

        public async Task<ActionResult> List()
        {
            var doctors =   _doctorService.GetAll();
            var viewModel = (from doctor in doctors
                                 select
              new DoctorViewModel()
              {
                  FIO = doctor.User.DisplayName,
                  Id = doctor.Id,
                  Specialization = doctor.Specialization.DisplayName,
                  Status = doctor.DoctorStatus.DisplayName,
                  CanStartChat = doctor.DoctorStatus.Name != DoctorStatusNames.Busy
              }).ToList();

            ViewBag.Specializations = await _specializationService.GetSpecializationsAsync();

            return View(viewModel);
        }

        public async Task<ActionResult> Index_Old()
        {
            var doctors = _doctorService.GetAll();
            var viewModel = (from doctor in doctors
                             select
          new DoctorViewModel()
          {
              FIO = doctor.User.DisplayName,
              Id = doctor.Id,
              Specialization = doctor.Specialization.DisplayName,
              Status = doctor.DoctorStatus.DisplayName,
              CanStartChat = doctor.DoctorStatus.Name != DoctorStatusNames.Busy
          }).ToList();

            ViewBag.Specializations = await _specializationService.GetSpecializationsAsync();

            return View("Index_Old", viewModel);
        }

        public async Task<ActionResult> StartDialog(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            var viewModel = new ChatDialogViewModel
            {
                DoctorDisplayName = doctor.User.DisplayName,
                DoctorUserId = doctor.UserId,
                Specialization = doctor.Specialization.DisplayName
            };
            
            return PartialView("_WaitDoctorAnswerDialog", viewModel);
        }

        public async Task<ActionResult> Appointment(int id)
        {
            AppointmentViewModel viewModel = new AppointmentViewModel();
            var doctor = await _doctorService.GetByIdAsync(id);
            viewModel.Date = DateTime.Today;
            viewModel.FIO = doctor.User.DisplayName;
            viewModel.Specialization = doctor.Specialization.DisplayName;
            viewModel.Status = doctor.DoctorStatus.DisplayName;
            viewModel.Id = id;
            return PartialView("_AppointmentDoctorDialog", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddAppointment(AppointmentViewModel viewModel)
        { 
            if (ModelState.IsValid)
            {
                AppointmentEvent appointmentEvent = new AppointmentEvent()
                {
                    Date = viewModel.Date,
                    Doctor = await _doctorService.GetByIdAsync(viewModel.Id),
                    Patient = await _patientService.GetByUserIdAsync(User.Identity.GetUserId()),
                    DateCreation = DateTime.Now
                };
                await _appointmentEventService.CreateAsync(appointmentEvent);
            }

            return PartialView("_AppointmentDoctorDialog", viewModel);
        }
    }
}