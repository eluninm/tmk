using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Hubs
{
    public interface ISignalClient
    {
        void OnCall(CallerInfo callerInfo);

        void OnCancelCall();

        void OnAcceptCall(string conversationId);

        void OnDeclineCall();

        void OnDoctorUpdated(DoctorListItemDto status);
    }
}