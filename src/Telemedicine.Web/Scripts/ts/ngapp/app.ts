///<reference path="Controllers/HistoryController.ts" />
///<reference path="Controllers/SimpleModalControllers.ts" />

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
        .service("recommendationService", RecommendationApiService)
        .service("urlResolverService", UrlResolverService)
        .service("patientApiService", PatientApiService)
        .service("consultationApiService", ConsultationApiService)
    ;
}