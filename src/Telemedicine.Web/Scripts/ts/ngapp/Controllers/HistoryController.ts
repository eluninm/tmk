///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
///<reference path="../Services/ConsultationApiService.ts"/>

module Telemedicine {
    export class HistoryController {
        static $inject = ["patientApiService", "consultationApiService", "$element", "$modal"];
        constructor(private patientApiService: PatientApiService,
                    private consultationApiService: ConsultationApiService,
                    private $element: ng.IAugmentedJQuery,
                    private $modal: ng.ui.bootstrap.IModalService) {
            var patientId = $element.attr("data-id");
            this.loadRecommendations(patientId);
            this.loadConsultations(patientId);
        }

        public recommendations: Array<IRecommendation>;
        public consultationMessages: Array<IConsultationMessage>;
        public consultations: Array<IConsultation>;

        public selectedConsultationId: string;

        public loadRecommendations(patientId: string) {
            this.patientApiService.patientRecommendations(patientId).then(result => {
                this.recommendations = result;
            });
        }

        public loadConsultations(patientId: string) {
            this.patientApiService.patientConsultations(patientId).then(result => {
                this.consultations = result;
            });
        }

        public loadConsultationMessages(consultationId: string) {
            this.consultationApiService.consultationMessages(consultationId).then(result => {
                this.consultationMessages = result;
            });
        }

        public consultationSelect(consultationId: string) {
            this.selectedConsultationId = consultationId;
            this.loadConsultationMessages(this.selectedConsultationId);
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