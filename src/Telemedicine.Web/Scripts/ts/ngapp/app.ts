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
///<reference path="Filters/ToDate.ts" /> 

module Telemedicine {
    function moduleConfiguration($logProvider: ng.ILogProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }

    angular.module("Telemedicine", ["ui.bootstrap"]).config(moduleConfiguration)
        .controller("PatientPaymentController", PatientPaymentController)
        .controller("DoctorPaymentController", DoctorPaymentController)
        .controller("HistoryController", HistoryController)
        .controller("ConsultationController", ConsultationController)
        .controller("RecommendationDetailsController", RecommendationDetailsController)
        .controller("DoctorListController", DoctorListController)
        .controller("DoctorDetailsController", DoctorDetailsController)
        .controller("AppointmentDialogController", AppointmentDialogController)
        .controller("PaymentDialogController", PaymentDialogController)
        .controller("BalanceController", BalanceController)
        .controller("DoctorAppointmentController", DoctorAppointmentController)
        .controller("PatientAppointmentController", PatientAppointmentController)
        .service("recommendationService", RecommendationApiService)
        .service("urlResolverService", UrlResolverService)
        .service("patientApiService", PatientApiService)
        .service("consultationApiService", ConsultationApiService)
        .service("doctorApiService", DoctorApiService)
        .service("specializationApiService", SpecializationApiService)
        .service("appointmentApiService", AppointmentApiService)
        .service("balanceApiService", BalanceApiService).filter("toDate", Telemedicine.ToDate);
}
    
