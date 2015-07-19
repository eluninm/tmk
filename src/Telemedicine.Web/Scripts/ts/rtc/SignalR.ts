//module RtcChat {
//    export interface ISignalR extends SignalR {
//        signalingHub: ISignalingHubProxy;
//        messageHub: IMessageHubProxy;
//    }

//    export interface ISignalingHubProxy extends HubProxy {
//        server: ISignalingServer;
//        client: ISignalingClient;
//    }

//    export interface IMessageHubProxy extends HubProxy {
//        server: IMessageServer;
//        client: IMessageClient;
//    }

//    export interface ISignalingServer {
//        startCall(roomId: string): JQueryPromise<void>;
//        endCall(roomId: string): JQueryPromise<void>;
//        acceptCall(): JQueryPromise<void>;
//        sendCandidate(roomId: string, candidate: string): JQueryPromise<void>;
//        sendDescription(roomId: string, description: string): JQueryPromise<void>;
//    }

//    export interface ISignalingClient {
//        onCall(roomId: string, callerInfo: CallerInfo);
//        onAnswer(roomId: string, accepted: boolean);
//        onCandidate(roomId: string, candidate: string);
//        onDescription(roomId: string, description: string);
//        onHang(roomId: string);
//    }

//    export interface IMessageServer {
//        getPreviousMessages(): Array<ChatTextMessage>;
//        sendMessage(message: string);
//    }

//    export interface IMessageClient {
//        onMessage(message: ChatTextMessage);
//    }

//    export class CallerInfo {
//        DisplayName: string;
//        CallerId: string;
//        AvatarUrl: string;
//    }

//    export class ChatTextMessage {
//        UserName: string;
//        UserDisplayName: string;
//        Message: string;
//    }
//}