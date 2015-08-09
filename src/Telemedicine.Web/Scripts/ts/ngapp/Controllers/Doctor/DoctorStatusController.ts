///<reference path="../../Services/DoctorApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class DoctorStatusController {
        static $inject = ["doctorApiService"];
        private doctorId: number;
        public doctorIsAvailable: boolean;

        constructor(private doctorApiService: DoctorApiService) {
            this.statusIsAvailable();
        }
        

        public statusIsAvailable() {
            this.doctorApiService.statusIsAvailable().then(result => {
                this.doctorIsAvailable = result;
                console.log("statusIsAvailable");
            });    
        }

        public changeStatus( ) {
            this.doctorApiService.changeDoctorStatus(!this.doctorIsAvailable).then(result => {
                this.statusIsAvailable();
            });
        }
    }
}