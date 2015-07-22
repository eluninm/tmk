///<reference path="../Services/PatientApiService.ts"/>

module Telemedicine {
    export class PatientPaymentListController {
        static $inject = ["patientApiService", "$scope"];

        constructor(private patientApiService: PatientApiService,
            private $scope: any) {
            this.getPaymentList();
            $scope.$on('ReplenishSuccess', this.getPaymentList);
        }

        public paymentsHistory: Array<IPaymentHistory>;

         


        public getPaymentList() {
            this.patientApiService.patientPaymentHistory().then(result => {
                this.paymentsHistory = result;
            });
        }
    }
}
 