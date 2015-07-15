using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models;

namespace Telemedicine.Web.Hubs
{
    public class TextChatMessage
    {
        public string UserDisplayName { get; set; }

        public string UserName { get; set; }

        public string Message { get; set; }

        public string Direction { get; set; }
    }

    public interface IMessageClient
    {
        Task OnMessage(TextChatMessage message);
    }

    [Microsoft.AspNet.SignalR.Authorize]
    public class MessageHub : Hub<IMessageClient>
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public async Task SendMessage(string conversationId, string messageText)
        {
            if (!string.IsNullOrWhiteSpace(messageText))
            {
                var conversationService = DependencyResolver.Current.GetService<IConversationService>();
                var doctorServoce = DependencyResolver.Current.GetService<IDoctorService>();
                var patientServcie = DependencyResolver.Current.GetService<IPatientService>();

                string userId = Context.User.Identity.GetUserId();
                var message = await conversationService.SendMessage(conversationId, userId, messageText);
                var textChatMessage = new TextChatMessage
                {
                    Message = message.Message,
                    UserDisplayName = message.Creator.DisplayName,
                    UserName = message.Creator.UserName
                };

                var conversation = await conversationService.OpenConversation(conversationId);
                foreach (var member in conversation.Members)
                {
                    var memberConnections = _connections.GetConnections(member.Id);
                    foreach (var memberConnectionId in memberConnections)
                    {
                        textChatMessage.Direction = memberConnectionId == Context.ConnectionId ? "me" : "target";
                        await Clients.Client(memberConnectionId).OnMessage(textChatMessage);
                    }
                }
            }
        }

        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();
            _connections.Add(userId, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = Context.User.Identity.GetUserId();
            _connections.Remove(userId, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string userId = Context.User.Identity.GetUserId();

            if (!_connections.GetConnections(userId).Contains(Context.ConnectionId))
            {
                _connections.Add(userId, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}