///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>

module Telemedicine {
    export class HistoryController {
        static $inject = ["patientApiService", "$element"];
        constructor(private patientApiService: PatientApiService,
                    private $element: ng.IAugmentedJQuery) {
            var patientId = $element.attr("data-id");
            this.loadRecommendations(patientId);
        }

        public recommendations: Array<Recommendation>;

        public loadRecommendations(patientId: string) {
            this.patientApiService.patientRecommendations(patientId).then(result => {
                this.recommendations = result;
            });
        }
    }
}