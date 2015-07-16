var Telemedicine;
(function (Telemedicine) {
    angular.module("Telemedicine").config(moduleConfiguration).controller("recommendationListController", Telemedicine.RecommendationListController).service("recommendationApiService", Telemedicine.RecommendationApiService);
    function moduleConfiguration($logProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    var ApiServiceBase = (function () {
        function ApiServiceBase(baseUrl, urlResolverService, $http) {
            this.baseUrl = baseUrl;
            this.urlResolverService = urlResolverService;
            this.$http = $http;
        }
        ApiServiceBase.prototype.getItems = function () {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        ApiServiceBase.prototype.getItem = function (itemId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + itemId);
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        return ApiServiceBase;
    })();
    Telemedicine.ApiServiceBase = ApiServiceBase;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    var ItemRouteParams = (function () {
        function ItemRouteParams() {
        }
        return ItemRouteParams;
    })();
    Telemedicine.ItemRouteParams = ItemRouteParams;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    var UrlResolverService = (function () {
        function UrlResolverService($rootElement) {
            this._base = $rootElement.attr("data-url-base");
            // Add trailing slash if not present
            if (this._base === "" || this._base.substr(this._base.length - 1) !== "/") {
                this._base = this._base + "/";
            }
        }
        Object.defineProperty(UrlResolverService.prototype, "base", {
            get: function () {
                return this._base;
            },
            enumerable: true,
            configurable: true
        });
        UrlResolverService.prototype.resolveUrl = function (relativeUrl) {
            var firstChar = relativeUrl.substr(0, 1);
            if (firstChar === "~") {
                relativeUrl = relativeUrl.substr(1);
            }
            firstChar = relativeUrl.substr(0, 1);
            if (firstChar === "/") {
                relativeUrl = relativeUrl.substr(1);
            }
            return this._base + relativeUrl;
        };
        return UrlResolverService;
    })();
    Telemedicine.UrlResolverService = UrlResolverService;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    var PatientApiService = (function () {
        function PatientApiService($http, urlResolverService) {
            this.$http = $http;
            this.urlResolverService = urlResolverService;
            this.baseUrl = "/api/v1/patients";
        }
        PatientApiService.prototype.patientRecommendations = function (patientId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/recommendations");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        return PatientApiService;
    })();
    Telemedicine.PatientApiService = PatientApiService;
})(Telemedicine || (Telemedicine = {}));
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Telemedicine;
(function (Telemedicine) {
    var RecommendationApiService = (function (_super) {
        __extends(RecommendationApiService, _super);
        function RecommendationApiService($http, urlResolverService) {
            _super.call(this, "~/api/v1/recommendations", urlResolverService, $http);
        }
        return RecommendationApiService;
    })(Telemedicine.ApiServiceBase);
    Telemedicine.RecommendationApiService = RecommendationApiService;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    var RecommendationListController = (function () {
        function RecommendationListController(recommendationApiService, $routeParams) {
            this.recommendationApiService = recommendationApiService;
            this.$routeParams = $routeParams;
        }
        return RecommendationListController;
    })();
    Telemedicine.RecommendationListController = RecommendationListController;
})(Telemedicine || (Telemedicine = {}));
var RtcChat;
(function (RtcChat) {
    var CallerInfo = (function () {
        function CallerInfo() {
        }
        return CallerInfo;
    })();
    RtcChat.CallerInfo = CallerInfo;
    var ChatTextMessage = (function () {
        function ChatTextMessage() {
        }
        return ChatTextMessage;
    })();
    RtcChat.ChatTextMessage = ChatTextMessage;
})(RtcChat || (RtcChat = {}));
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
//# sourceMappingURL=ngapp.js.map