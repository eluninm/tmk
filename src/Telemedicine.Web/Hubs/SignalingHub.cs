using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Telemedicine.Web.Hubs
{
    public class SignalingHub : Hub<ISignalingClient>, ISignalingServer
    {
        public void StartPeerCall(string peerId)
        {
            Clients.AllExcept(Context.ConnectionId).PeerCall(new CallerInfo
            {
                DisplayName = "Иванов Иван Иванович",
            });
        }

        public void AcceptCall()
        {
            Clients.AllExcept(Context.ConnectionId).OnCallAccepted();
        }

        public void SendCandidate(string roomId, string candidate)
        {
            Clients.AllExcept(Context.ConnectionId).OnCandidate(roomId, candidate);
        }

        public void SendDescription(string roomId, string description)
        {
            Clients.AllExcept(Context.ConnectionId).OnDescription(roomId, description);
        }

        public void EndPeerCall(string roomId)
        {
            Clients.All.PeerHang();
        }

        public override Task OnConnected()
        {
            Clients.AllExcept(Context.ConnectionId).OnConnected(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Clients.AllExcept(Context.ConnectionId).OnDisconnected(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }

    public interface ISignalingServer
    {
        void StartPeerCall(string peerId);

        void EndPeerCall(string peerId);

        void AcceptCall();

        void SendCandidate(string peerId, string candidate);

        void SendDescription(string roomId, string description);

        Task OnConnected();
    }

    public interface ISignalingClient
    {
        void PeerCall(CallerInfo callerInfo);

        void OnCallAccepted();

        void OnCallDeclined();

        void OnCandidate(string roomId, string candidate);

        void OnDescription(string roomId, string description);

        void PeerHang();

        void OnConnected(string clientId);

        void OnDisconnected(string clientId);
    }

    public class CallerInfo
    {
        public string DisplayName { get; set; }

        public string CallerId { get; set; }

        public string AvatarUrl { get; set; }

        public string GroupId { get; set; }
    }
}