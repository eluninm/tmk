using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;

namespace Telemedicine.Web.Hubs.Interfaces
{
    public interface ISignalServer
    {
        Task BeginConsultation(int doctorId);
    }

    public class SignalServer : ISignalServer
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly SiteUserManager _userManager;
        private IHubContext _context;

        public SignalServer(IDoctorService doctorService, IPatientService patientService, SiteUserManager userManager)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _userManager = userManager;
            _context = GlobalHost.ConnectionManager.GetHubContext<SignalHub>();
        }

        public async Task BeginConsultation(int patientId, int doctorId)
        {
            var callerPatient = await _patientService.GetByIdAsync(patientId);
            
        }
    }
}