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
var __extends = (this && this.__extends) || function (d, b) {
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
            _super.call(this, "~/api/v1/recommendation", urlResolverService, $http);
        }
        RecommendationApiService.prototype.addRecommendation = function (patientId, recommendationText) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/addRecommendation/" + patientId + "/" + recommendationText);
            return this.$http.post(url, { patientId: patientId, recommendationText: recommendationText }).then(function (result) { return result.data; });
        };
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
        function PatientApiService($http, urlResolverService, $rootScope) {
            this.$http = $http;
            this.urlResolverService = urlResolverService;
            this.$rootScope = $rootScope;
            this.baseUrl = "/api/v1/patient";
        }
        PatientApiService.prototype.patientRecommendations = function (patientId, doctorId) {
            var query = {}, querySeparator = "?";
            var url = this.baseUrl + "/" + patientId + "/recommendations";
            query.patientId = patientId;
            if (doctorId) {
                query.doctorId = doctorId;
            }
            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }
            //var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/recommendations");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        PatientApiService.prototype.patientConsultations = function (patientId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/consultations");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        PatientApiService.prototype.getPaymentHistory = function (patientId, page, pageSize) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/paymentHistory");
            var query = {}, querySeparator = "?";
            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        PatientApiService.prototype.getPatientAppointments = function (patientId, page, pageSize) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/appointments");
            var query = {}, querySeparator = "?";
            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        PatientApiService.prototype.payConsultation = function (doctorId, value) {
            var querySeparator = "/";
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/payConsultation/" + encodeURIComponent(String(doctorId)) + querySeparator + encodeURIComponent(String(value)));
            return this.$http.post(url, { "doctorId": doctorId, "value": value }).then(function (result) { return result.data; });
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
            this.baseUrl = "/api/v1/consultation";
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
            var doctorId = $element.attr("data-doctor-id");
            this.loadRecommendations(patientId, doctorId);
            this.loadConsultations(patientId);
        }
        HistoryController.prototype.loadRecommendations = function (patientId, doctorId) {
            var _this = this;
            this.patientApiService.patientRecommendations(patientId, Number(doctorId)).then(function (result) {
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
        HistoryController.prototype.openAddRecommendation = function () {
            var _this = this;
            var addDialog = this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/addRecommendationDialog.html",
                controller: "RecommendationDetailsController as viewModel",
                resolve: {
                    item: function () { return null; }
                }
            });
            addDialog.result.then(function (result) {
                _this.recommendationService.addRecommendation(Number(_this.$element.attr("data-id")), result).then(function (result) {
                    _this.loadRecommendations(_this.$element.attr("data-id"), _this.$element.attr("data-doctor-id"));
                });
            });
        };
        HistoryController.$inject = ["patientApiService", "consultationApiService", "recommendationService", "$element", "$modal"];
        return HistoryController;
    })();
    Telemedicine.HistoryController = HistoryController;
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
                    item: function () { return null; }
                }
            });
        };
        ConsultationController.$inject = ["patientApiService", "consultationApiService", "$element", "$modal"];
        return ConsultationController;
    })();
    Telemedicine.ConsultationController = ConsultationController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/> 
///<reference path="../../dts/moment.d.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorApiService = (function () {
        function DoctorApiService($http, urlResolverService) {
            this.$http = $http;
            this.urlResolverService = urlResolverService;
            this.baseUrl = "/api/v1/doctor";
        }
        DoctorApiService.prototype.getDoctorList = function (page, pageSize, title, specializationId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            var query = {}, querySeparator = "?";
            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            if (title) {
                query.titleFilter = title;
            }
            if (specializationId) {
                query.specializationIdFilter = specializationId;
            }
            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.getDoctorDetails = function (doctorId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/details");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.getDoctorAppointments = function (doctorId, page, pageSize, patientTitleFilter, start, end, needDeclined, needReady, needClosed) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/appointments");
            var query = {}, querySeparator = "?";
            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            if (patientTitleFilter) {
                query.patientTitleFilter = patientTitleFilter;
            }
            if (start) {
                query.start = moment(start).utc().toISOString();
            }
            if (end) {
                query.end = moment(end).utc().toISOString();
            }
            if (needDeclined) {
                query.needDeclined = needDeclined;
            }
            if (needReady) {
                query.needReady = needReady;
            }
            if (needClosed) {
                query.needClosed = needClosed;
            }
            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.getDoctorTimeWindows = function (doctorId) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/timeWindows");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.getPaymentHistory = function (doctorId, page, pageSize, start, end) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/paymentHistory");
            var query = {}, querySeparator = "?";
            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            if (start && end) {
                query.start = start.toISOString();
                query.end = end.toISOString();
            }
            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.getDoctorTimelineByMonth = function (doctorId, year, month) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/timeline/" + year + "/" + month);
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.changeDoctorStatus = function (isAvailable) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + isAvailable + "/changeStatus");
            return this.$http.post(url, { "isAvailable": isAvailable }).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.changeHourStatus = function (date, selectedHour, status) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/changeHourStatus/" + date.getFullYear() + "/" +
                (date.getMonth() + 1) + "/" +
                date.getDate() + "/" +
                selectedHour + "/" +
                status);
            return this.$http.post(url, {
                "year": date.getFullYear(),
                "month": (date.getMonth() + 1),
                "day": date.getDate(),
                "selectedHour": selectedHour,
                "status": status
            }).then(function (result) { return result.data; });
        };
        DoctorApiService.prototype.statusIsAvailable = function () {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/statusIsAvailable");
            return this.$http.get(url).then(function (result) { return result.data; });
        };
        return DoctorApiService;
    })();
    Telemedicine.DoctorApiService = DoctorApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var SpecializationApiService = (function (_super) {
        __extends(SpecializationApiService, _super);
        function SpecializationApiService($http, urlResolverService) {
            _super.call(this, "~/api/v1/specialization", urlResolverService, $http);
        }
        SpecializationApiService.$inject = ["$http", "urlResolverService"];
        return SpecializationApiService;
    })(Telemedicine.ApiServiceBase);
    Telemedicine.SpecializationApiService = SpecializationApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var AppointmentApiService = (function () {
        function AppointmentApiService($http, urlResolverService) {
            this.$http = $http;
            this.urlResolverService = urlResolverService;
            this.baseUrl = "/api/v1/appointment";
            this.timeout = 10000;
        }
        AppointmentApiService.prototype.createItem = function (item, config) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            this.$http.post(url, item, config || { timeout: this.timeout }).then(function (result) { return result.data; });
        };
        return AppointmentApiService;
    })();
    Telemedicine.AppointmentApiService = AppointmentApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="ISignalClient.ts"/>
///<reference path="ISignalServer.ts"/>
///<reference path="Appointment/IAppointmentClient.ts"/>
///<reference path="Appointment/IAppointmentServer.ts"/>
///<reference path="../Services/DoctorApiService.ts"/>
///<reference path="../Services/SpecializationApiService.ts"/>
///<reference path="../Services/AppointmentApiService.ts"/>
///<reference path="../SignalR/SignalR.ts"/> 
///<reference path="../Services/PatientApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorListController = (function () {
        function DoctorListController(patientApiService, doctorApiService, specializationApiService, appointmentApiService, $modal, $scope) {
            this.patientApiService = patientApiService;
            this.doctorApiService = doctorApiService;
            this.specializationApiService = specializationApiService;
            this.appointmentApiService = appointmentApiService;
            this.$modal = $modal;
            this.$scope = $scope;
            this.currentPage = 1;
            this.pageSize = 1;
            this.loadPage();
            this.loadSpecializations();
            this.initialize();
            //this.appointmentHub.client = $.connection.appointmentHub.client;
            //this.appointmentHub.server = $.connection.appointmentHub.server;
        }
        DoctorListController.prototype.loadPage = function (pageToLoad) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            var sid = this.selectedSpecialization ? this.selectedSpecialization.Id : 0;
            this.doctorApiService.getDoctorList(page, this.pageSize, this.doctorTitleFilter, sid).then(function (result) {
                _this.doctors = result.Data;
                _this.totalCount = result.TotalCount;
                _this.currentPage = result.Page;
                _this.pageSize = result.PageSize;
            });
        };
        DoctorListController.prototype.loadSpecializations = function () {
            var _this = this;
            this.specializationApiService.getItems().then(function (result) {
                _this.specializations = result;
            });
        };
        DoctorListController.prototype.selectSpecialization = function (specialization) {
            this.selectedSpecialization = specialization;
            this.loadPage();
        };
        DoctorListController.prototype.changePageSize = function (size) {
            this.pageSize = size;
            this.loadPage();
        };
        DoctorListController.prototype.openDetailsDialog = function (doctor) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/doctorDetails.html",
                controller: "DoctorDetailsController as viewModel",
                windowClass: "infomodaldialog",
                resolve: {
                    item: function () { return doctor; }
                }
            });
        };
        DoctorListController.prototype.openAppointmentDialog = function (doctor) {
            var _this = this;
            var appointment = { DoctorId: doctor.Id, AppointmentDate: new Date() };
            this.doctorApiService.getDoctorTimeWindows(doctor.Id).then(function (result) {
                var appointmentDialog = _this.$modal.open({
                    templateUrl: "/Content/tmpls/dialogs/appointmentDialog.html",
                    controller: "AppointmentDialogController as viewModel",
                    windowClass: "appointmentmodaldialog",
                    resolve: {
                        item: function () { return doctor; },
                        appointment: function () { return appointment; },
                        timeWindows: function () { return result; }
                    }
                });
                appointmentDialog.result.then(function (result) {
                    if (result === "appointment") {
                        _this.appointmentApiService.createItem(appointment);
                        appointmentDialog.close();
                        _this.openPaymentDialog(doctor);
                        console.log("Send doctor id:" + doctor.Id);
                    }
                });
            });
        };
        DoctorListController.prototype.openPaymentDialog = function (doctor) {
            var _this = this;
            console.log("openPaymentDialog doctor id:" + doctor.Id);
            var paymentDialog = this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/paymentDialog.html",
                controller: "PaymentDialogController as viewModel",
                windowClass: "paymentmodaldialog",
                resolve: {
                    item: function () { return null; }
                }
            });
            paymentDialog.result.then(function (result) {
                console.log(result);
                _this.patientApiService.payConsultation(doctor.Id, 1000);
            });
        };
        DoctorListController.prototype.initialize = function () {
            var _this = this;
            var signal = $.connection.signalHub;
            signal.client.onDoctorUpdated = function (s) { return _this.onDoctorUpdated(s); };
        };
        DoctorListController.prototype.onDoctorUpdated = function (doctor) {
            var _this = this;
            for (var i = 0; i < this.doctors.length; i++) {
                if (this.doctors[i].Id == doctor.Id) {
                    this.$scope.$apply(function () {
                        _this.doctors[i] = doctor;
                    });
                }
            }
        };
        DoctorListController.$inject = ["patientApiService", "doctorApiService", "specializationApiService", "appointmentApiService", "$modal", "$scope"];
        return DoctorListController;
    })();
    Telemedicine.DoctorListController = DoctorListController;
})(Telemedicine || (Telemedicine = {}));
/// <reference path="../../dts/angular-ui-bootstrap.d.ts" />
var Telemedicine;
(function (Telemedicine) {
    var ItemModalViewModel = (function () {
        function ItemModalViewModel($modalInstance, item) {
            this.$modalInstance = $modalInstance;
            this.item = item;
        }
        ItemModalViewModel.prototype.ok = function (result) {
            this.$modalInstance.close(result);
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
///<reference path="../common/ModalControllerBase.ts"/>
///<reference path="../Services/DoctorApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorDetailsController = (function (_super) {
        __extends(DoctorDetailsController, _super);
        function DoctorDetailsController(doctorApiService, $modalInstance, item) {
            _super.call(this, $modalInstance, item);
            this.doctorApiService = doctorApiService;
            this.loadDetails(item.Id);
        }
        DoctorDetailsController.prototype.loadDetails = function (doctorId) {
            var _this = this;
            this.doctorApiService.getDoctorDetails(doctorId).then(function (result) {
                _this.doctorDetails = result;
            });
        };
        return DoctorDetailsController;
    })(Telemedicine.ItemModalViewModel);
    Telemedicine.DoctorDetailsController = DoctorDetailsController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ModalControllerBase.ts"/>
///<reference path="../../dts/bootstrap.v3.datetimepicker.d.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var AppointmentDialogController = (function (_super) {
        __extends(AppointmentDialogController, _super);
        function AppointmentDialogController($modalInstance, item, appointment, timeWindows) {
            _super.call(this, $modalInstance, item);
            this.appointment = appointment;
            this.timeWindows = timeWindows;
        }
        AppointmentDialogController.prototype.initDatetimepicker = function () {
            $("#datetimepicker1").datetimepicker({
                inline: true,
                sideBySide: true,
                locale: 'ru',
                minDate: this.timeWindows.MinDate.toString(),
                maxDate: this.timeWindows.MaxDate.toString(),
                stepping: this.timeWindows.WindowSize,
                defaultDate: this.timeWindows.NearestAvailable.toString(),
                icons: {
                    up: "glyphicon glyphicon-triangle-top",
                    down: "glyphicon glyphicon-triangle-bottom",
                    next: "glyphicon glyphicon-triangle-right",
                    previous: "glyphicon glyphicon-triangle-left"
                }
            });
        };
        AppointmentDialogController.prototype.book = function () {
            this.appointment.AppointmentDate = $("#datetimepicker1").datetimepicker().data("date");
            this.ok("appointment");
        };
        return AppointmentDialogController;
    })(Telemedicine.ItemModalViewModel);
    Telemedicine.AppointmentDialogController = AppointmentDialogController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ModalControllerBase.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var PaymentDialogController = (function (_super) {
        __extends(PaymentDialogController, _super);
        function PaymentDialogController($modalInstance, item) {
            _super.call(this, $modalInstance, item);
        }
        return PaymentDialogController;
    })(Telemedicine.ItemModalViewModel);
    Telemedicine.PaymentDialogController = PaymentDialogController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var BalanceApiService = (function () {
        function BalanceApiService($http, urlResolverService) {
            this.$http = $http;
            this.urlResolverService = urlResolverService;
            this.baseUrl = "/api/v1/balance";
        }
        BalanceApiService.prototype.debit = function (amount) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/debit/" + amount);
            return this.$http.post(url, null).then(function (result) { return result.data; });
        };
        BalanceApiService.prototype.replenish = function (amount) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/replenish/" + amount);
            return this.$http.post(url, null).then(function (result) { return result.data; });
        };
        BalanceApiService.prototype.balance = function () {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/get");
            return this.$http.get(url, null).then(function (result) { return result.data; });
        };
        return BalanceApiService;
    })();
    Telemedicine.BalanceApiService = BalanceApiService;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../Services/BalanceApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var BalanceController = (function () {
        function BalanceController(patientApiService, balanceApiService, $scope) {
            var _this = this;
            this.patientApiService = patientApiService;
            this.balanceApiService = balanceApiService;
            this.$scope = $scope;
            this.getBalance();
            $scope.$on('child', function (event, data) { return _this.balanceUpdatedEventHandler(event, data); });
        }
        BalanceController.prototype.debit = function (amount) {
            var _this = this;
            this.balanceApiService.debit(amount).then(function (result) {
                _this.getBalance();
            });
        };
        BalanceController.prototype.replenish = function (amount) {
            var _this = this;
            this.balanceApiService.replenish(amount).then(function (result) {
                _this.debitValue = 0;
                _this.getBalance();
            });
        };
        BalanceController.prototype.balanceUpdatedEventHandler = function (event, data) {
            this.balance2 = data;
            this.$scope.$digest();
        };
        BalanceController.prototype.getBalance = function () {
            var _this = this;
            this.balanceApiService.balance().then(function (result) {
                _this.balance2 = result;
            });
        };
        BalanceController.$inject = ["patientApiService", "balanceApiService", "$scope"];
        return BalanceController;
    })();
    Telemedicine.BalanceController = BalanceController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../Services/DoctorApiService.ts"/>
///<reference path="../../dts/moment.d.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorAppointmentController = (function () {
        function DoctorAppointmentController(doctorApiService, $modal, $element) {
            this.doctorApiService = doctorApiService;
            this.$modal = $modal;
            this.$element = $element;
            this.currentPage = 1;
            this.pageSize = 10;
            this.doctorId = parseInt($element.attr("data-id"));
            if (location.hash) {
                var parameters = this.parseUrlQuery();
                this.start = moment(parameters["start"]).toDate();
                this.end = moment(parameters["end"]).toDate();
            }
        }
        DoctorAppointmentController.prototype.parseUrlQuery = function () {
            var data = new Array();
            if (location.hash) {
                var pair = (location.hash.substr(1)).split('&');
                for (var i = 0; i < pair.length; i++) {
                    var param = pair[i].split('=');
                    data[param[0]] = param[1];
                }
            }
            return data;
        };
        DoctorAppointmentController.prototype.setStart = function (start) {
            this.start = start;
        };
        DoctorAppointmentController.prototype.setEnd = function (end) {
            this.end = end;
        };
        DoctorAppointmentController.prototype.loadPage = function (pageToLoad) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter, this.start, this.end, false, true, true).then(function (result) {
                _this.appointments = result.Data;
                _this.totalCount = result.TotalCount;
                _this.currentPage = result.Page;
                _this.pageSize = result.PageSize;
            });
        };
        DoctorAppointmentController.prototype.loadAppointmentPageWithStatusEqualsReady = function (pageToLoad) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter, this.start, this.end, false, true, false).then(function (result) {
                _this.appointments = result.Data;
                _this.totalCount = result.TotalCount;
                _this.currentPage = result.Page;
                _this.pageSize = result.PageSize;
            });
        };
        DoctorAppointmentController.prototype.loadAppointmentPageWithStatusEqualsClosed = function (pageToLoad) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter, this.start, this.end, false, false, true).then(function (result) {
                _this.appointments = result.Data;
                _this.totalCount = result.TotalCount;
                _this.currentPage = result.Page;
                _this.pageSize = result.PageSize;
            });
        };
        DoctorAppointmentController.prototype.statusIsReady = function (status) {
        };
        DoctorAppointmentController.prototype.getStatusText = function (status) {
            console.log(status);
            switch (status) {
                case Telemedicine.AppointmentStatus.Ready:
                    {
                        return "Отмена";
                    }
                case Telemedicine.AppointmentStatus.Closed:
                    {
                        return "«Консультация завершена»";
                    }
                case Telemedicine.AppointmentStatus.Declined:
                    {
                        return "«Консультация отмена»";
                    }
            }
        };
        DoctorAppointmentController.prototype.changePageSize = function (size) {
            this.pageSize = size;
            this.loadPage();
        };
        DoctorAppointmentController.$inject = ["doctorApiService", "$modal", "$element"];
        return DoctorAppointmentController;
    })();
    Telemedicine.DoctorAppointmentController = DoctorAppointmentController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../../Services/PatientApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var PatientAppointmentController = (function () {
        function PatientAppointmentController(patientApiService, $modal, $element) {
            this.patientApiService = patientApiService;
            this.$modal = $modal;
            this.$element = $element;
            this.currentPage = 1;
            this.pageSize = 1;
            this.patientId = parseInt($element.attr("data-id"));
            this.loadPage();
        }
        PatientAppointmentController.prototype.loadPage = function (pageToLoad) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            this.patientApiService.getPatientAppointments(this.patientId, page, this.pageSize).then(function (result) {
                _this.appointments = result.Data;
                _this.totalCount = result.TotalCount;
                _this.currentPage = result.Page;
                _this.pageSize = result.PageSize;
            });
        };
        PatientAppointmentController.prototype.changePageSize = function (size) {
            this.pageSize = size;
            this.loadPage();
        };
        PatientAppointmentController.$inject = ["patientApiService", "$modal", "$element"];
        return PatientAppointmentController;
    })();
    Telemedicine.PatientAppointmentController = PatientAppointmentController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../../Services/PatientApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorPaymentController = (function () {
        function DoctorPaymentController(doctorApiService, balanceApiService, $element) {
            this.doctorApiService = doctorApiService;
            this.balanceApiService = balanceApiService;
            this.$element = $element;
            this.currentPage = 1;
            this.pageSize = 10;
            this.doctorId = parseInt($element.data("id"));
            this.loadPage();
            this.loadBalance();
        }
        DoctorPaymentController.prototype.loadPage = function (pageToLoad, isReload) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            var currentPageSize = this.pageSize;
            if (isReload || typeof (this.allPayments) == 'undefined') {
                this.doctorApiService.getPaymentHistory(this.doctorId, null, null, this.start, this.end).then(function (result) {
                    _this.allPayments = result.Data;
                    _this.payments = _this.allPayments.slice((page - 1) * currentPageSize, page * currentPageSize);
                    _this.totalCount = result.TotalCount;
                    _this.currentPage = page;
                    _this.totalBalance = 0;
                    for (var i in _this.allPayments) {
                        _this.totalBalance += _this.allPayments[i].Value;
                    }
                });
            }
            else {
                this.payments = this.allPayments.slice((page - 1) * currentPageSize, page * currentPageSize);
                this.currentPage = page;
            }
        };
        DoctorPaymentController.prototype.debit = function () {
            var _this = this;
            if (this.debitValue && this.debitValue > 0) {
                this.balanceApiService.debit(this.debitValue).then(function (result) {
                    _this.loadBalance();
                    _this.loadPage();
                    _this.debitValue = null;
                });
            }
        };
        DoctorPaymentController.prototype.setStart = function (start) {
            this.start = start;
        };
        DoctorPaymentController.prototype.setEnd = function (end) {
            this.end = end;
        };
        DoctorPaymentController.prototype.loadBalance = function () {
            var _this = this;
            this.balanceApiService.balance().then(function (result) {
                _this.balance = result;
            });
        };
        DoctorPaymentController.prototype.changePageSize = function (size) {
            this.pageSize = size;
            this.loadPage(1);
        };
        DoctorPaymentController.$inject = ["doctorApiService", "balanceApiService", "$element"];
        return DoctorPaymentController;
    })();
    Telemedicine.DoctorPaymentController = DoctorPaymentController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../Services/PatientApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var PatientPaymentController = (function () {
        function PatientPaymentController(patientApiService, balanceApiService, $element, $scope) {
            this.patientApiService = patientApiService;
            this.balanceApiService = balanceApiService;
            this.$element = $element;
            this.$scope = $scope;
            this.patientId = parseInt($element.data("id"));
            this.loadPage();
            this.loadBalance();
        }
        PatientPaymentController.prototype.loadPage = function (pageToLoad) {
            var _this = this;
            var page = pageToLoad || this.currentPage;
            this.patientApiService.getPaymentHistory(this.patientId, page, this.pageSize).then(function (result) {
                _this.payments = result.Data;
                _this.totalCount = result.TotalCount;
                _this.currentPage = result.Page;
                _this.pageSize = result.PageSize;
            });
        };
        PatientPaymentController.prototype.replenish = function () {
            var _this = this;
            if (this.paymentValue && this.paymentValue > 0) {
                this.balanceApiService.replenish(this.paymentValue).then(function (result) {
                    _this.loadBalance();
                    _this.loadPage();
                    _this.paymentValue = null;
                });
            }
        };
        PatientPaymentController.prototype.loadBalance = function () {
            var _this = this;
            this.balanceApiService.balance().then(function (result) {
                _this.balance = result;
                _this.$scope.$emit('child', _this.balance);
            });
        };
        PatientPaymentController.$inject = ["patientApiService", "balanceApiService", "$element", "$scope"];
        return PatientPaymentController;
    })();
    Telemedicine.PatientPaymentController = PatientPaymentController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../../Services/DoctorApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/> 
///<reference path="../../../dts/moment.d.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorTimelineController = (function () {
        function DoctorTimelineController(doctorApiService, balanceApiService, $element, $scope) {
            this.doctorApiService = doctorApiService;
            this.balanceApiService = balanceApiService;
            this.$element = $element;
            this.$scope = $scope;
            this.year = 2015;
            this.month = 8;
            this.curentDate = new Date();
            this.doctorId = parseInt($element.data("id"));
            this.loadTimeline();
            this.loadBalance();
        }
        DoctorTimelineController.prototype.loadTimeline = function () {
            var _this = this;
            //this.year = 2015;
            //this.month = 8;
            this.doctorApiService.getDoctorTimelineByMonth(this.doctorId, this.year, this.month).then(function (result) {
                _this.timeLineDates = result;
                _this.updateTimeLine();
                document.getElementById("patientListOnCurrentDayLink").click();
            });
        };
        DoctorTimelineController.prototype.loadBalance = function () {
            var _this = this;
            this.balanceApiService.balance().then(function (result) {
                _this.balance = result;
            });
        };
        DoctorTimelineController.prototype.selectHour = function (hour) {
            this.selectedHour = hour;
        };
        DoctorTimelineController.prototype.availableHour = function () {
            var _this = this;
            this.doctorApiService.changeHourStatus(this.curentDate, this.selectedHour, Telemedicine.TimelineHourType.Working).then(function (result) {
                _this.loadTimeline();
            });
        };
        DoctorTimelineController.prototype.unavailableHour = function () {
            var _this = this;
            this.doctorApiService.changeHourStatus(this.curentDate, this.selectedHour, Telemedicine.TimelineHourType.NotWorking).then(function (result) {
                _this.loadTimeline();
            });
        };
        DoctorTimelineController.prototype.clearHour = function () {
            var _this = this;
            this.doctorApiService.changeHourStatus(this.curentDate, this.selectedHour, Telemedicine.TimelineHourType.Clear).then(function (result) {
                _this.loadTimeline();
            });
        };
        DoctorTimelineController.prototype.updateTimeLine = function () {
            console.log("updateTimeLine");
            for (var i = 0; i < this.timeLineDates.length; i++) {
                var date = Number(this.timeLineDates[i].Date.toString().substr(8, 2));
                if (date == this.curentDate.getDate()) {
                    this.currentTimeLine = this.timeLineDates[i];
                    if (!this.$scope.$$phase) {
                        this.$scope.$apply();
                    }
                }
            }
        };
        DoctorTimelineController.prototype.range = function (n) {
            return new Array(n);
        };
        DoctorTimelineController.prototype.changeDate = function (date) {
            this.curentDate = date;
            console.log("changeDate");
            if ((date.getMonth() + 1) != this.month || date.getFullYear() != this.year) {
                this.month = date.getMonth() + 1;
                this.year = date.getFullYear();
                this.loadTimeline();
                console.log("month is changed");
            }
            else {
                this.updateTimeLine();
            }
        };
        DoctorTimelineController.prototype.showMyEvents = function () {
            var start = moment(new Date(this.curentDate.setHours(this.selectedHour))).utc().startOf('day');
            var end = moment(new Date(this.curentDate.setHours(this.selectedHour))).utc().endOf('day');
            window.location.href = "/Doctor/Home/MyEvents#start=" + start.toISOString() + "&end=" + end.toISOString();
        };
        DoctorTimelineController.$inject = ["doctorApiService", "balanceApiService", "$element", "$scope"];
        return DoctorTimelineController;
    })();
    Telemedicine.DoctorTimelineController = DoctorTimelineController;
})(Telemedicine || (Telemedicine = {}));
///<reference path="../../Services/DoctorApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorStatusController = (function () {
        function DoctorStatusController(doctorApiService) {
            this.doctorApiService = doctorApiService;
            this.statusIsAvailable();
        }
        DoctorStatusController.prototype.statusIsAvailable = function () {
            var _this = this;
            this.doctorApiService.statusIsAvailable().then(function (result) {
                _this.doctorIsAvailable = result;
            });
        };
        DoctorStatusController.prototype.changeStatus = function () {
            var _this = this;
            this.doctorApiService.changeDoctorStatus(!this.doctorIsAvailable).then(function (result) {
                _this.statusIsAvailable();
            });
        };
        DoctorStatusController.$inject = ["doctorApiService"];
        return DoctorStatusController;
    })();
    Telemedicine.DoctorStatusController = DoctorStatusController;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    function ToDate() {
        return function (input) {
            return new Date(input);
        };
    }
    Telemedicine.ToDate = ToDate;
})(Telemedicine || (Telemedicine = {}));
///<reference path="Controllers/HistoryController.ts" />
///<reference path="Controllers/ConsultationController.ts" />
///<reference path="Controllers/DoctorListController.ts" />
///<reference path="Controllers/SimpleModalControllers.ts" />
///<reference path="Controllers/DoctorDetailsController.ts" />
///<reference path="Controllers/AppointmentDialogController.ts" />
///<reference path="Controllers/PaymentDialogController.ts" />
///<reference path="Controllers/BalanceController.ts" />
///<reference path="Controllers/DoctorAppointmentController.ts" />
///<reference path="Controllers/Patient/PatientAppointmentController.ts" /> 
///<reference path="Controllers/Doctor/DoctorPaymentController.ts" />
///<reference path="Controllers/PatientPaymentController.ts" /> 
///<reference path="Controllers/Doctor/DoctorTimelineController.ts" /> 
///<reference path="Controllers/Doctor/DoctorStatusController.ts" /> 
///<reference path="Filters/ToDate.ts" /> 
var Telemedicine;
(function (Telemedicine) {
    function moduleConfiguration($logProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }
    angular.module("Telemedicine", ["ui.bootstrap"]).config(moduleConfiguration)
        .controller("PatientPaymentController", Telemedicine.PatientPaymentController)
        .controller("DoctorPaymentController", Telemedicine.DoctorPaymentController)
        .controller("DoctorTimelineController", Telemedicine.DoctorTimelineController)
        .controller("DoctorStatusController", Telemedicine.DoctorStatusController)
        .controller("HistoryController", Telemedicine.HistoryController)
        .controller("ConsultationController", Telemedicine.ConsultationController)
        .controller("RecommendationDetailsController", Telemedicine.RecommendationDetailsController)
        .controller("DoctorListController", Telemedicine.DoctorListController)
        .controller("DoctorDetailsController", Telemedicine.DoctorDetailsController)
        .controller("AppointmentDialogController", Telemedicine.AppointmentDialogController)
        .controller("PaymentDialogController", Telemedicine.PaymentDialogController)
        .controller("BalanceController", Telemedicine.BalanceController)
        .controller("DoctorAppointmentController", Telemedicine.DoctorAppointmentController)
        .controller("PatientAppointmentController", Telemedicine.PatientAppointmentController)
        .service("recommendationService", Telemedicine.RecommendationApiService)
        .service("urlResolverService", Telemedicine.UrlResolverService)
        .service("patientApiService", Telemedicine.PatientApiService)
        .service("consultationApiService", Telemedicine.ConsultationApiService)
        .service("doctorApiService", Telemedicine.DoctorApiService)
        .service("specializationApiService", Telemedicine.SpecializationApiService)
        .service("appointmentApiService", Telemedicine.AppointmentApiService)
        .service("balanceApiService", Telemedicine.BalanceApiService).filter("toDate", Telemedicine.ToDate);
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
///<reference path="../../Services/DoctorApiService.ts"/>
var Telemedicine;
(function (Telemedicine) {
    var DoctorHourStatusController = (function () {
        function DoctorHourStatusController(doctorApiService) {
            this.doctorApiService = doctorApiService;
        }
        DoctorHourStatusController.prototype.changeStatus = function (doctorIsAvailable) {
            this.doctorApiService.changeDoctorStatus(doctorIsAvailable);
        };
        DoctorHourStatusController.prototype.changeStatus1 = function (doctorIsAvailable) {
            this.doctorApiService.changeDoctorStatus(doctorIsAvailable);
        };
        DoctorHourStatusController.$inject = ["doctorApiService"];
        return DoctorHourStatusController;
    })();
    Telemedicine.DoctorHourStatusController = DoctorHourStatusController;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    (function (AppointmentStatus) {
        AppointmentStatus[AppointmentStatus["Declined"] = 0] = "Declined";
        AppointmentStatus[AppointmentStatus["Closed"] = 1] = "Closed";
        AppointmentStatus[AppointmentStatus["Ready"] = 2] = "Ready";
    })(Telemedicine.AppointmentStatus || (Telemedicine.AppointmentStatus = {}));
    var AppointmentStatus = Telemedicine.AppointmentStatus;
})(Telemedicine || (Telemedicine = {}));
var Telemedicine;
(function (Telemedicine) {
    (function (TimelineHourType) {
        TimelineHourType[TimelineHourType["Working"] = 0] = "Working";
        TimelineHourType[TimelineHourType["NotWorking"] = 1] = "NotWorking";
        TimelineHourType[TimelineHourType["Clear"] = 2] = "Clear";
    })(Telemedicine.TimelineHourType || (Telemedicine.TimelineHourType = {}));
    var TimelineHourType = Telemedicine.TimelineHourType;
})(Telemedicine || (Telemedicine = {}));
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
//# sourceMappingURL=ngapp.js.map