///<reference path="../Services/RecommendationApiService.ts"/>
///<reference path="../Services/PatientApiService.ts"/>
///<reference path="../Services/ConsultationApiService.ts"/>

module Telemedicine {
    export class DoctorListController {
        static $inject = ["patientApiService", "consultationApiService", "recommendationService", "$element", "$modal"];

        constructor(private patientApiService: PatientApiService,
            private consultationApiService: ConsultationApiService,
            private recommendationService: RecommendationApiService,
            private $element: ng.IAugmentedJQuery,
            private $modal: ng.ui.bootstrap.IModalService) {
            this.loadDoctors();
        }

        public doctors: Array<IDoctorListItem>;

        public loadDoctors() {

        }

        public openDoctorDetails(recommendation: IRecommendation) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/recommendationDetails.html",
                controller: "RecommendationDetailsController as viewModel",
                resolve: {
                    item: () => recommendation
                }
            });
        }
    }
}