using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Data;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Api.Dto;
using Telemedicine.Web.Areas.Doctor.Models;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    [System.Web.Mvc.Authorize(Roles = UserRoleNames.Doctor)]
    public class StatusController : Controller
    {
        private readonly IDoctorService _service;

        public StatusController(IDoctorService service)
        {
            _service = service;
        }

        //http://aspnetwebstack.codeplex.com/workitem/601
        public ActionResult GetStatus()
        {
            var currentUserId = User.Identity.GetUserId();
            var status = _service.GetStatusByUserId(currentUserId);
            var available = _service.GetAvailableStatuses();
            return PartialView("Status", new DoctorStatusViewModel {CurrentStatus = status, AvailableStatuses = available});
        }

        public async Task<ActionResult> ChangeStatus(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var status = _service.SetStatusByUserId(currentUserId, id);
            var available = _service.GetAvailableStatuses();

            var doctor = await _service.GetByUserIdAsync(currentUserId);
            var doctorDto = Mapper.Map<DoctorListItemDto>(doctor);
            doctorDto.IsChatAvailable = doctor.DoctorStatus.Name != DoctorStatusNames.Busy;
            doctorDto.IsAudioAvailable = doctor.DoctorStatus.Name == DoctorStatusNames.VideoChat;
            doctorDto.IsVideoAvailable = doctor.DoctorStatus.Name == DoctorStatusNames.VideoChat;



            GlobalHost.ConnectionManager.GetHubContext<SignalHub>()
                .Clients.All.OnDoctorUpdated(doctorDto);


            if (Request.IsAjaxRequest())
            {
                return PartialView("Status", new DoctorStatusViewModel { CurrentStatus = status, AvailableStatuses = available });
            }

            return View("Status", new DoctorStatusViewModel { CurrentStatus = status, AvailableStatuses = available });
        }
    }
}