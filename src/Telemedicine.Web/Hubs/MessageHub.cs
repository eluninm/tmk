using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Hubs
{
    public interface IMessageClient
    {
        Task OnMessage(ConsultationMessageDto messageDto);
    }

    [Microsoft.AspNet.SignalR.Authorize]
    public class MessageHub : Hub<IMessageClient>
    {
        #region Private

        private static readonly ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        #endregion

        public async Task SendMessage(string conversationId, string messageText)
        {
            if (!string.IsNullOrWhiteSpace(messageText))
            {
                IConversationService conversationService = DependencyResolver.Current.GetService<IConversationService>();
                IDoctorService doctorServoce = DependencyResolver.Current.GetService<IDoctorService>();
                IPatientService patientServcie = DependencyResolver.Current.GetService<IPatientService>();

                string userId = Context.User.Identity.GetUserId();
                var message = await conversationService.SendMessage(conversationId, userId, messageText);
                ConsultationMessageDto textChatMessage = new ConsultationMessageDto
                {
                    Message = message.Message,
                    UserDisplayName = message.Creator.DisplayName,
                    UserName = message.Creator.UserName,
                    Created = DateTime.Now
                };

                var conversation = await conversationService.OpenConversation(conversationId);
                foreach (var member in conversation.Members)
                {
                    IEnumerable<string> memberConnections = _connections.GetConnections(member.Id);
                    foreach (string memberConnectionId in memberConnections)
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