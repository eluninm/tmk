///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
///<reference path="../Services/ConsultationApiService.ts"/> 

module Telemedicine {
    export class HistoryController {
        static $inject = ["patientApiService", "consultationApiService", "recommendationService", "$element", "$modal"];

        constructor(private patientApiService: PatientApiService,
            private consultationApiService: ConsultationApiService,
            private recommendationService: RecommendationApiService,
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

        public openRecommendationDetails(recommendation: IRecommendation) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/recommendationDetails.html",
                controller: "RecommendationDetailsController as viewModel",
                resolve: {
                    item: () => recommendation
                }
            });

        } 

        public openAddRecommendation() {
            var addDialog = this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/addRecommendationDialog.html",
                controller: "RecommendationDetailsController as viewModel",
                resolve: {
                    item: () => null
                }
            });

            addDialog.result.then((result) => {
                this.recommendationService.addRecommendation(Number(this.$element.attr("data-id")), result).then(result => {
                    this.loadRecommendations(this.$element.attr("data-id"));
                });
            });
        }
    }
}