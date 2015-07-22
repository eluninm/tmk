///<reference path="Controllers/HistoryController.ts" />
///<reference path="Controllers/ConsultationController.ts" />
///<reference path="Controllers/DoctorListController.ts" />
///<reference path="Controllers/SimpleModalControllers.ts" />
///<reference path="Controllers/DoctorDetailsController.ts" />
///<reference path="Controllers/AppointmentDialogController.ts" />
///<reference path="Controllers/PaymentDialogController.ts" />
///<reference path="Controllers/BalanceController.ts" />
///<reference path="Controllers/DoctorAppointmentsController.ts" />
///<reference path="Controllers/PatientPaymentListController.ts" />

module Telemedicine {
    function moduleConfiguration($logProvider: ng.ILogProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }

    angular.module("Telemedicine", ["ui.bootstrap"]).config(moduleConfiguration)
        .controller("PatientPaymentListController", PatientPaymentListController) 
        .controller("HistoryController", HistoryController)
        .controller("ConsultationController", ConsultationController)
        .controller("RecommendationDetailsController", RecommendationDetailsController)
        .controller("DoctorListController", DoctorListController)
        .controller("DoctorDetailsController", DoctorDetailsController)
        .controller("AppointmentDialogController", AppointmentDialogController)
        .controller("PaymentDialogController", PaymentDialogController)
        .controller("BalanceController", BalanceController)
        .controller("DoctorAppointmentsController", DoctorAppointmentsController)
        .service("recommendationService", RecommendationApiService)
        .service("urlResolverService", UrlResolverService)
        .service("patientApiService", PatientApiService)
        .service("consultationApiService", ConsultationApiService)
        .service("doctorApiService", DoctorApiService)
        .service("specializationApiService", SpecializationApiService)
        .service("appointmentApiService", AppointmentApiService)
        .service("balanceApiService", BalanceApiService)
    ;
}