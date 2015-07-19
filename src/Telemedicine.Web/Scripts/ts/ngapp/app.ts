///<reference path="Controllers/HistoryController.ts" />
///<reference path="Controllers/ConsultationController.ts" />
///<reference path="Controllers/DoctorListController.ts" />
///<reference path="Controllers/SimpleModalControllers.ts" />
///<reference path="Controllers/DoctorDetailsController.ts" />

module Telemedicine {
    function moduleConfiguration($logProvider: ng.ILogProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }

    angular.module("Telemedicine", ["ui.bootstrap"]).config(moduleConfiguration)
        .controller("HistoryController", HistoryController)
        .controller("ConsultationController", ConsultationController)
        .controller("RecommendationDetailsController", RecommendationDetailsController)
        .controller("DoctorListController", DoctorListController)
        .controller("DoctorDetailsController", DoctorDetailsController)
        .service("recommendationService", RecommendationApiService)
        .service("urlResolverService", UrlResolverService)
        .service("patientApiService", PatientApiService)
        .service("consultationApiService", ConsultationApiService)
        .service("doctorApiService", DoctorApiService)
        .service("specializationApiService", SpecializationApiService)
    ;
}