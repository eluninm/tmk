///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
///<reference path="../Services/ConsultationApiService.ts"/>

module Telemedicine {
    export class ConsultationController {
        static $inject = ["patientApiService", "consultationApiService", "$element", "$modal"];
        constructor(private patientApiService: PatientApiService,
            private consultationApiService: ConsultationApiService,
            private $element: ng.IAugmentedJQuery,
            private $modal: ng.ui.bootstrap.IModalService) {
            var patientId = $element.attr("data-patient-id");
            var consultationId = $element.attr("data-consultation-id");
            this.loadRecommendations(patientId);
        }

        public recommendations: Array<IRecommendation>;
        public consultationMessages: Array<IConsultationMessage>;
        public consultations: Array<IConsultation>;

        public loadRecommendations(patientId: string) {
            this.patientApiService.patientRecommendations(patientId).then(result => {
                this.recommendations = result;
            });
        }

        public loadConsultationMessages(consultationId: string) {
            this.consultationApiService.consultationMessages(consultationId).then(result => {
                this.consultationMessages = result;
            });
        }

        public openRecommendationDetails(recommendationId: string) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/recommendationDetails.html",
                controller: "RecommendationDialog as viewModel",
                resolve: {
                    item: () => item
                }
            });
        }
    }
}