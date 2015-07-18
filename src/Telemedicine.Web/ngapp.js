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
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
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
        RecommendationApiService.$inject = ["$http", "urlResolverService"];
        return RecommendationApiService;
    })(Telemedicine.ApiServiceBase);
    Telemedicine.RecommendationApiService = RecommendationApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
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
        PatientApiService.prototype.patientConsultations = function (patientId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/consultations");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        return PatientApiService;
    })();
    Telemedicine.PatientApiService = PatientApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var ConsultationApiService = (function () {
        function ConsultationApiService($http, urlResolverService) {
            this.$http = $http;
            this.urlResolverService = urlResolverService;
            this.baseUrl = "/api/v1/consultations";
        }
        ConsultationApiService.prototype.consultationMessages = function (consultationId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + consultationId + "/messages");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        return ConsultationApiService;
    })();
    Telemedicine.ConsultationApiService = ConsultationApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
///<reference path="../Services/ConsultationApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var HistoryController = (function () {
        function HistoryController(patientApiService, consultationApiService, recommendationService, $element, $modal) {
            this.patientApiService = patientApiService;
            this.consultationApiService = consultationApiService;
            this.recommendationService = recommendationService;
            this.$element = $element;
            this.$modal = $modal;
            var patientId = $element.attr("data-id");
            this.loadRecommendations(patientId);
            this.loadConsultations(patientId);
        }
        HistoryController.prototype.loadRecommendations = function (patientId) {
            var _this = this;
            this.patientApiService.patientRecommendations(patientId).then(function (result) {
                _this.recommendations = result;
            });
        };
        HistoryController.prototype.loadConsultations = function (patientId) {
            var _this = this;
            this.patientApiService.patientConsultations(patientId).then(function (result) {
                _this.consultations = result;
            });
        };
        HistoryController.prototype.loadConsultationMessages = function (consultationId) {
            var _this = this;
            this.consultationApiService.consultationMessages(consultationId).then(function (result) {
                _this.consultationMessages = result;
            });
        };
        HistoryController.prototype.consultationSelect = function (consultationId) {
            this.selectedConsultationId = consultationId;
            this.loadConsultationMessages(this.selectedConsultationId);
        };
        HistoryController.prototype.openRecommendationDetails = function (recommendation) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/recommendationDetails.html",
                controller: "RecommendationDetailsController as viewModel",
                resolve: {
                    item: function () { return recommendation; }
                }
            });
        };
        HistoryController.$inject = ["patientApiService", "consultationApiService", "recommendationService", "$element", "$modal"];
        return HistoryController;
    })();
    Telemedicine.HistoryController = HistoryController;
})(Telemedicine || (Telemedicine = {}));
/// <reference path="../../dts/angular-ui-bootstrap.d.ts" />
var Telemedicine;
(function (Telemedicine) {
    var ItemModalViewModel = (function () {
        function ItemModalViewModel($modalInstance, item) {
            this.$modalInstance = $modalInstance;
            this.item = item;
        }
        ItemModalViewModel.prototype.ok = function () {
            this.$modalInstance.close(true);
        };
        ItemModalViewModel.prototype.cancel = function () {
            this.$modalInstance.dismiss("cancel");
        };
        return ItemModalViewModel;
    })();
    Telemedicine.ItemModalViewModel = ItemModalViewModel;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ModalControllerBase.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var RecommendationDetailsController = (function (_super) {
        __extends(RecommendationDetailsController, _super);
        function RecommendationDetailsController($modalInstance, item) {
            _super.call(this, $modalInstance, item);
        }
        return RecommendationDetailsController;
    })(Telemedicine.ItemModalViewModel);
    Telemedicine.RecommendationDetailsController = RecommendationDetailsController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="Controllers/HistoryController.ts" />
///<reference path="Controllers/SimpleModalControllers.ts" />
var Telemedicine;
(function (Telemedicine) {
    function moduleConfiguration($logProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }
    angular.module("Telemedicine", ["ui.bootstrap"]).config(moduleConfiguration).controller("HistoryController", Telemedicine.HistoryController).controller("ConsultationController", Telemedicine.ConsultationController).controller("RecommendationDetailsController", Telemedicine.RecommendationDetailsController).service("recommendationService", Telemedicine.RecommendationApiService).service("urlResolverService", Telemedicine.UrlResolverService).service("patientApiService", Telemedicine.PatientApiService).service("consultationApiService", Telemedicine.ConsultationApiService);
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
///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
///<reference path="../Services/ConsultationApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var ConsultationController = (function () {
        function ConsultationController(patientApiService, consultationApiService, $element, $modal) {
            this.patientApiService = patientApiService;
            this.consultationApiService = consultationApiService;
            this.$element = $element;
            this.$modal = $modal;
            var patientId = $element.attr("data-patient-id");
            var consultationId = $element.attr("data-consultation-id");
            this.loadRecommendations(patientId);
        }
        ConsultationController.prototype.loadRecommendations = function (patientId) {
            var _this = this;
            this.patientApiService.patientRecommendations(patientId).then(function (result) {
                _this.recommendations = result;
            });
        };
        ConsultationController.prototype.loadConsultationMessages = function (consultationId) {
            var _this = this;
            this.consultationApiService.consultationMessages(consultationId).then(function (result) {
                _this.consultationMessages = result;
            });
        };
        ConsultationController.prototype.openRecommendationDetails = function (recommendationId) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/recommendationDetails.html",
                controller: "RecommendationDialog as viewModel",
                resolve: {
                    item: function () { return item; }
                }
            });
        };
        ConsultationController.$inject = ["patientApiService", "consultationApiService", "$element", "$modal"];
        return ConsultationController;
    })();
    Telemedicine.ConsultationController = ConsultationController;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    var TestService = (function () {
        function TestService() {
        }
        return TestService;
    })();
    Telemedicine.TestService = TestService;
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