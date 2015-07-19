module Telemedicine {
    export interface ISignalServer {
        startCall(roomId: string): JQueryPromise<void>;
        endCall(roomId: string): JQueryPromise<void>;
        acceptCall(): JQueryPromise<void>;
        sendCandidate(roomId: string, candidate: string): JQueryPromise<void>;
        sendDescription(roomId: string, description: string): JQueryPromise<void>;
    }
}