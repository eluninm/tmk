///<reference path="../Services/PatientApiService.ts"/>

module Telemedicine {
    export class PatientPaymentListController {
        static $inject = ["patientApiService"];

        constructor(private patientApiService: PatientApiService) {
            this.getPaymentList();
        }

        public paymentsHistory: Array<IPaymentHistory>;

        public getPaymentList() {
            this.patientApiService.patientPaymentHistory().then(result => {
                this.paymentsHistory = result;
            });
        }
    }
}
 