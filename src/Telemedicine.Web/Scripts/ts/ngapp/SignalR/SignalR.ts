///<reference path="ISignalClient.ts"/>
///<reference path="ISignalServer.ts"/>

    interface SignalR {
        signalHub: HubProxy;
    }

    interface HubProxy {
        server: Telemedicine.ISignalServer;
        client: Telemedicine.ISignalClient;
    }
