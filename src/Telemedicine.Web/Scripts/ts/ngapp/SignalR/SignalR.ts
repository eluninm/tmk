///<reference path="ISignalClient.ts"/>
///<reference path="ISignalServer.ts"/>
///<reference path="Appointment/IAppointmentClient.ts"/>
///<reference path="Appointment/IAppointmentServer.ts"/>

interface SignalR {
    signalHub: HubProxy;
    appointmentHub: IAppointmentProxy;
}

interface HubProxy {
    server: Telemedicine.ISignalServer;
    client: Telemedicine.ISignalClient;
}

interface IAppointmentProxy {
    server: Telemedicine.IAppointmentServer;
    client: Telemedicine.IAppointmentClient;
}