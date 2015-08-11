using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
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

        public Task CloseChat()
        {
            return Clients.All.OnChatClosed();
        }

        public Task SendRTCMessage(object e)
        {
            return Clients.Others.OnRTCMessage(e);
        }

        public Task InitiateMediaChat(bool withVideo)
        {
            var callerUserId = Context.User.Identity.GetUserId();
            return Clients.Others.OnMediaChatRequest(callerUserId, withVideo);
        }

        public Task CancelMediaChatRequest()
        {
            return Clients.Others.OnCancelMediaChatRequest();
        }

        public Task AcceptMediaChat(bool withVideo)
        {
            return Clients.Others.OnMediaChatAccepted(withVideo);
        }

        public Task DeclineMediaChat()
        {
            return Clients.Others.OnMediaChatDeclined();
        }

    }

    public interface IChatServer
    {
        Task UserStatusChanged(string statusName);
        Task CloseChat();
    }

    public interface IChatClient
    {
        Task Connected(string userName);

        Task Disconnected(string userName);

        Task StatusChanged(string statusName);

        Task OnChatClosed();

        Task OnRTCMessage(object e);

        Task OnMediaChatRequest(string initiatorId, bool withVideo);

        Task OnMediaChatAccepted(bool withVideo);

        Task OnMediaChatDeclined();

        Task OnCancelMediaChatRequest();
    }
}