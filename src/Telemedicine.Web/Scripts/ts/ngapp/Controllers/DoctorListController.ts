///<reference path="../Services/DoctorApiService.ts"/>
///<reference path="../SignalR/SignalR.ts"/>

module Telemedicine {
    export class DoctorListController {
        static $inject = ["doctorApiService", "$modal", "$scope"];

        constructor(private doctorApiService: DoctorApiService,
            private $modal: ng.ui.bootstrap.IModalService, private $scope: any) {
            this.loadDoctors();
            this.initializeSignaling();
        }

        public doctors: Array<IDoctor>;

        public loadDoctors() {
            this.doctorApiService.getDoctorList().then(result => {
                this.doctors = result;
            });
        }

        public openDoctorDetails(doctor: IDoctor) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/recommendationDetails.html",
                controller: "RecommendationDetailsController as viewModel",
                resolve: {
                    item: () => doctor
                }
            });
        }

        initializeSignaling() {
            var signal = $.connection.signalHub;
            signal.client.onDoctorUpdated = (s) => this.onDoctorStatusChanged(s);
        }

        onDoctorStatusChanged(doctor: IDoctor) {
            for (var i = 0; i < this.doctors.length; i++) {
                if (this.doctors[i].Id == doctor.Id) {
                    this.$scope.$apply(() => {
                        this.doctors[i] = doctor;
                    });
                }
            }
        }
    }
}