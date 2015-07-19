///// <reference path="SignalR.ts" />

//module RtcChat {
//    export interface IVideoChat {
//        startConnection(roomId: string);
//        stopConnection(roomId: string);
//    }

//    export class VideoChat implements IVideoChat {
//        private peerConnection: RTCPeerConnection;
//        private roomId: string;

//        constructor(private config: RTCConfiguration, private signalr: ISignalR) {
//            this.ensureSignaling();
//        }

//        startConnection(roomId: string): void {
//            if (this.peerConnection == null && this.roomId == null) {
//                this.peerConnection = this.createConnection();
//                this.roomId = roomId;
//                if (this.onConnectionReady != null) {
//                    this.onConnectionReady();
//                }
//            }
//        }

//        stopConnection(roomId: string): void {
//            this.roomId = null;
//            if (this.peerConnection != null) {
//                this.peerConnection.close();
//                this.peerConnection = null;
//            }
//        }

//        private ensureSignaling() {
//            this.signalr.signalingHub.client.onDescription = (roomId, remoteDescriptionString) => {
//                var remoteDescription = new RTCSessionDescription($.parseJSON(remoteDescriptionString));
//                this.onRemoteDescription(remoteDescription);
//            }

//            this.signalr.signalingHub.client.onCandidate = (roomId, remoteCandidateString) => {
//                var iceCandidate = new RTCIceCandidate($.parseJSON(remoteCandidateString));
//                this.onRemoteCandidate(iceCandidate);
//            }
//        }

//        private createConnection(): RTCPeerConnection {
//            var connection = new RTCPeerConnection(this.config);
//            connection.onicecandidate = c => this.onLocalCandidate(c);
//            connection.onaddstream = s => this.onStreamAdded(s);
//            connection.onsignalingstatechange = () => {
//                console.info("Signaling state: %s", connection.signalingState);
//            };
//            connection.oniceconnectionstatechange = () => {
//                console.info("Connection state: %s", connection.iceConnectionState);
//            };
//            connection.onnegotiationneeded = () => {
//                console.warn("Negatiation needed!");
//                this.peerConnection.createOffer((sd) => this.onLocalDescription(sd), this.onError);
//            };

//            return connection;
//        }

//        private onLocalDescription(sessionDescription: RTCSessionDescription) {
//            this.peerConnection.setLocalDescription(sessionDescription, () => {
//                this.signalr.signalingHub.server.sendDescription(this.roomId, JSON.stringify(this.peerConnection.localDescription));
//                if (sessionDescription.type === "offer") {
//                    console.info("Offer sended");
//                }
//                if (sessionDescription.type === "answer") {
//                    console.info("Answer sended");
//                }
//            }, this.onError);
//        }

//        private onRemoteDescription(sessionDescription: RTCSessionDescription) {
//            this.ensureConnection();

//            this.peerConnection.setRemoteDescription(sessionDescription, () => {
//                if (sessionDescription.type === "offer") {
//                    this.peerConnection.createAnswer((sd) => this.onLocalDescription(sd), this.onError);
//                    console.info("Offer received");
//                }
//                if (sessionDescription.type === "answer") {
//                    console.info("Answer received");
//                }
//            }, this.onError);
//        }

//        private onLocalCandidate(event: RTCIceCandidateEvent): void {
//            if (event.candidate) {
//                this.signalr.signalingHub.server.sendCandidate(this.roomId, JSON.stringify(event.candidate));
//                console.info("Local ICE candidate occur: %s", event.candidate.candidate);
//            }
//        }

//        private onRemoteCandidate(iceCandidate: RTCIceCandidate) {
//            this.peerConnection.addIceCandidate(iceCandidate, () => {
//                console.info("Remote ICE candidate occur: %s", iceCandidate.candidate);
//            }, this.onError);
//        }

//        private onError(error: DOMError) {
//            console.error(error);
//        }

//        onStreamAdded(event: RTCMediaStreamEvent): void {
//            if (this.onStreamReceived != null) {
//                this.onStreamReceived(event.stream);
//            }
//        }

//        private addStream = (stream: MediaStream) => {
//            this.peerConnection.addStream(stream);
//        };

//        onConnectionReady;
//        onStreamReceived;

//        private ensureConnection() {
//            if (this.peerConnection == null) {
//                this.startConnection("");
//            }
//        }
//    }
//}