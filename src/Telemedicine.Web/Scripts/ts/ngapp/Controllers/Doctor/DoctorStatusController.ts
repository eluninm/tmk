///<reference path="../../Services/DoctorApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class DoctorStatusController {
        static $inject = ["doctorApiService"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService) {
            
        }
        

        public changeStatus(doctorIsAvailable:boolean) {
            this.doctorApiService.changeDoctorStatus(doctorIsAvailable);
        }
    }
}