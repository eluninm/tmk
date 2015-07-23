///<reference path="../Services/PatientApiService.ts"/>

module Telemedicine {
    export class DoctorPaymentController {
        static $inject = ["patientApiService", "$scope"];

        constructor(private patientApiService: PatientApiService,
            private $scope: any) {  
        }

        public paymentsHistory: Array<IPaymentHistory>;

        

        /*public getPaymentList() {
            this.patientApiService.patientPaymentHistory().then(result => {
                this.paymentsHistory = result;
                this.$scope.$apply();
            });
        }*/
    }
}
 