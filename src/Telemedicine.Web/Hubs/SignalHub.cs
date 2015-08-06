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
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Hubs
{
    public class CallResult
    {
        public bool Success { get; set; }

        public string GroupId { get; set; }
    }

    [Microsoft.AspNet.SignalR.Authorize]
    public class SignalHub : Hub<ISignalClient>
    {
        public readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        private readonly ILifetimeScope _hubLifetimeScope;

        public SignalHub()
        {
            //_hubLifetimeScope = lifetimeScope.BeginLifetimeScope();
        }

        public CallResult StartCall(string targetUserId)
        {
            var groupId = Guid.NewGuid().ToString();
            Groups.Add(Context.ConnectionId, groupId);

            var callerUserId = Context.User.Identity.GetUserId();
            var callerInfo = new CallerInfo
            {
                DisplayName = Context.User.Identity.GetUserDisplayName(),
                CallerId = callerUserId,
                GroupId = groupId
            };

            //if user not connected - return false
            if (!_connections.GetConnections(targetUserId).Any())
            {
                return new CallResult {Success = false};
            }

            foreach (var connectionId in _connections.GetConnections(targetUserId))
            {
                Groups.Add(connectionId, groupId);
                Clients.Client(connectionId).OnCall(callerInfo);
            }

            return new CallResult {Success = true, GroupId = groupId};
        }

        public void CancelCall(string groupId)
        {
            Clients.OthersInGroup(groupId).OnCancelCall();
        }

        public async Task<string> AcceptCall(string groupId)
        {
            try
            {
                string userId = Context.User.Identity.GetUserId();
                var peerUser = _connections.GetUserIds().FirstOrDefault(t => t != Context.User.Identity.GetUserId());

                var conversationService = DependencyResolver.Current.GetService<IConversationService>();
                var conversation = await conversationService.BeginConversation(peerUser, userId);

                Clients.OthersInGroup(groupId).OnAcceptCall(conversation.Id);
                return conversation.Id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeclineCall(string groupId)
        {
            Clients.OthersInGroup(groupId).OnDeclineCall();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hubLifetimeScope?.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    public interface ISignalClient
    {
        void OnCall(CallerInfo callerInfo);

        void OnCancelCall();

        void OnAcceptCall(string conversationId);

        void OnDeclineCall();

        void OnDoctorUpdated(DoctorListItemDto status);
    }
}