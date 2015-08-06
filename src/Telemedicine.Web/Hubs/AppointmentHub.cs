using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Domain.Services;

namespace Telemedicine.Web.Hubs
{
    public class AppointmentHub:Hub<IAppointmentClient> , IAppointmentServer
    {
        //private readonly static Dictionary<string,string> _connections = new Dictionary<string, string>();
        
        ////public async Task Connected()
        ////{
        ////    string userId = Context.User.Identity.GetUserId(); 
        ////}

        ////public async Task  Disconnected(bool stopCalled)
        ////{
        ////    string userId = Context.User.Identity.GetUserId();
        ////}
 
        public void NewAppointment(string userId)
        {
            foreach (string id in SignalHub._connections.GetConnections(userId))
            {
                Clients.Client(id).OnNewAppointment();
            } 
        }
    }

    public interface IAppointmentClient
    {
        void OnNewAppointment();
    }

    public interface IAppointmentServer
    {
        void NewAppointment(string doctorId);
    }
}