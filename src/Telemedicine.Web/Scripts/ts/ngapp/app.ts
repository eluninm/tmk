module Telemedicine {
    angular.module("Telemedicine").config(moduleConfiguration)
        .controller("recommendationListController", RecommendationListController)
        .service("recommendationApiService", RecommendationApiService)
    ;

    function moduleConfiguration($logProvider: ng.ILogProvider) {
        // TODO: Enable debug logging based on server config
        // TODO: Capture all logged errors and send back to server
        $logProvider.debugEnabled(true);
    }
}