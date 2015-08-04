///<reference path="../../Services/DoctorApiService.ts"/>

module Telemedicine {
    export class DoctorHourStatusController {
        static $inject = ["doctorApiService"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService) {

        }


        public changeStatus(doctorIsAvailable: boolean) {
            this.doctorApiService.changeDoctorStatus(doctorIsAvailable);
        }
    }
}