/// <reference path="dts/MediaStream.d.ts" />
/// <reference path="dts/RTCPeerConnection.d.ts" />
/// <reference path="SignalR.ts" />
var RtcChat;
(function (RtcChat) {
    var VideoChat = (function () {
        function VideoChat(config, signalr) {
            var _this = this;
            this.config = config;
            this.signalr = signalr;
            this.addStream = function (stream) {
                _this.peerConnection.addStream(stream);
            };
            this.ensureSignaling();
        }
        VideoChat.prototype.startConnection = function (roomId) {
            if (this.peerConnection == null && this.roomId == null) {
                this.peerConnection = this.createConnection();
                this.roomId = roomId;
                if (this.onConnectionReady != null) {
                    this.onConnectionReady();
                }
            }
        };
        VideoChat.prototype.stopConnection = function (roomId) {
            this.roomId = null;
            if (this.peerConnection != null) {
                this.peerConnection.close();
                this.peerConnection = null;
            }
        };
        VideoChat.prototype.ensureSignaling = function () {
            var _this = this;
            this.signalr.signalingHub.client.onDescription = function (roomId, remoteDescriptionString) {
                var remoteDescription = new RTCSessionDescription($.parseJSON(remoteDescriptionString));
                _this.onRemoteDescription(remoteDescription);
            };
            this.signalr.signalingHub.client.onCandidate = function (roomId, remoteCandidateString) {
                var iceCandidate = new RTCIceCandidate($.parseJSON(remoteCandidateString));
                _this.onRemoteCandidate(iceCandidate);
            };
        };
        VideoChat.prototype.createConnection = function () {
            var _this = this;
            var connection = new RTCPeerConnection(this.config);
            connection.onicecandidate = function (c) { return _this.onLocalCandidate(c); };
            connection.onaddstream = function (s) { return _this.onStreamAdded(s); };
            connection.onsignalingstatechange = function () {
                console.info("Signaling state: %s", connection.signalingState);
            };
            connection.oniceconnectionstatechange = function () {
                console.info("Connection state: %s", connection.iceConnectionState);
            };
            connection.onnegotiationneeded = function () {
                console.warn("Negatiation needed!");
                _this.peerConnection.createOffer(function (sd) { return _this.onLocalDescription(sd); }, _this.onError);
            };
            return connection;
        };
        VideoChat.prototype.onLocalDescription = function (sessionDescription) {
            var _this = this;
            this.peerConnection.setLocalDescription(sessionDescription, function () {
                _this.signalr.signalingHub.server.sendDescription(_this.roomId, JSON.stringify(_this.peerConnection.localDescription));
                if (sessionDescription.type === "offer") {
                    console.info("Offer sended");
                }
                if (sessionDescription.type === "answer") {
                    console.info("Answer sended");
                }
            }, this.onError);
        };
        VideoChat.prototype.onRemoteDescription = function (sessionDescription) {
            var _this = this;
            this.ensureConnection();
            this.peerConnection.setRemoteDescription(sessionDescription, function () {
                if (sessionDescription.type === "offer") {
                    _this.peerConnection.createAnswer(function (sd) { return _this.onLocalDescription(sd); }, _this.onError);
                    console.info("Offer received");
                }
                if (sessionDescription.type === "answer") {
                    console.info("Answer received");
                }
            }, this.onError);
        };
        VideoChat.prototype.onLocalCandidate = function (event) {
            if (event.candidate) {
                this.signalr.signalingHub.server.sendCandidate(this.roomId, JSON.stringify(event.candidate));
                console.info("Local ICE candidate occur: %s", event.candidate.candidate);
            }
        };
        VideoChat.prototype.onRemoteCandidate = function (iceCandidate) {
            this.peerConnection.addIceCandidate(iceCandidate, function () {
                console.info("Remote ICE candidate occur: %s", iceCandidate.candidate);
            }, this.onError);
        };
        VideoChat.prototype.onError = function (error) {
            console.error(error);
        };
        VideoChat.prototype.onStreamAdded = function (event) {
            if (this.onStreamReceived != null) {
                this.onStreamReceived(event.stream);
            }
        };
        VideoChat.prototype.ensureConnection = function () {
            if (this.peerConnection == null) {
                this.startConnection("");
            }
        };
        return VideoChat;
    })();
    RtcChat.VideoChat = VideoChat;
})(RtcChat || (RtcChat = {}));
//# sourceMappingURL=ChatManager.js.map