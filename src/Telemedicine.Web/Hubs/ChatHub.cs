﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Telemedicine.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>, IChatServer
    {
        public override Task OnConnected()
        {
            var userName = Context.User.Identity.Name;
            Clients.All.Connected(userName);
            
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = Context.User.Identity.Name;
            Clients.All.Disconnected(userName);
            
            return base.OnDisconnected(stopCalled);
        }

        public Task UserStatusChanged(string statusName)
        {
            return Clients.All.StatusChanged(statusName);
        }
  
        public void CancelChat(string doctorUserId)
        {
            foreach (var connectionId in SignalHub._connections.GetConnections(doctorUserId))
            {
                Clients.Client(connectionId).OnChatClosed();
            } 
        }
    }

    public interface IChatServer
    {
        Task UserStatusChanged(string statusName); 
        void CancelChat(string groupId);
    }

    public interface IChatClient
    {
        Task Connected(string userName);

        Task Disconnected(string userName);

        Task StatusChanged(string statusName); 

        Task OnChatClosed();
    }
}