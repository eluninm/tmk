using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Consts;
using Telemedicine.Core.Data;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Areas.Doctor.Models;
using Telemedicine.Web.Hubs;

namespace Telemedicine.Web.Areas.Doctor.Controllers
{
    [System.Web.Mvc.Authorize(Roles = UserRoleNames.Doctor)]
    public class StatusController : Controller
    {
        private readonly IDoctorService _service;
        private readonly IDbContextProvider _provider;

        public StatusController(IDoctorService service, IDbContextProvider provider)
        {
            _service = service;
            _provider = provider;
        }

        //http://aspnetwebstack.codeplex.com/workitem/601
        public ActionResult GetStatus()
        {
            var currentUserId = User.Identity.GetUserId();
            var status = _service.GetStatusByUserId(currentUserId);
            var available = _service.GetAvailableStatuses();
            return PartialView("Status", new DoctorStatusViewModel {CurrentStatus = status, AvailableStatuses = available});
        }

        public ActionResult ChangeStatus(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var status = _service.SetStatusByUserId(currentUserId, id);
            var available = _service.GetAvailableStatuses();

            var doctor = _provider.Context.Set<Core.Models.Doctor>().Include("DoctorStatus").FirstOrDefault(t => t.UserId == currentUserId);

            GlobalHost.ConnectionManager.GetHubContext<SignalHub>()
                .Clients.All.OnDoctorChangeStatus(new DoctorChangeStatus
                {
                    DoctorId = doctor.Id.ToString(),
                    StatusText = doctor.DoctorStatus.DisplayName,
                    CanCall = doctor.DoctorStatus.Name != DoctorStatusNames.Busy,
                    StatusName = doctor.DoctorStatus.Name
                });

            if (Request.IsAjaxRequest())
            {
                return PartialView("Status", new DoctorStatusViewModel { CurrentStatus = status, AvailableStatuses = available });
            }

            return View("Status", new DoctorStatusViewModel { CurrentStatus = status, AvailableStatuses = available });
        }
    }
}