///<reference path="../Services/DoctorApiService.ts"/>

module Telemedicine {
    export class DoctorAppointmentController {
        static $inject = ["doctorApiService", "$modal", "$element"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService,
            private $modal: ng.ui.bootstrap.IModalService,
            private $element: ng.IAugmentedJQuery) {
            this.doctorId = parseInt($element.attr("data-id"));
            this.loadPage();
        }

        public appointments: Array<IDoctorAppointment>;
        public patientTitleFilter: string;

        public totalCount: number;
        public currentPage: number = 1;
        public pageSize: number = 10;
        public start: Date;
        public end: Date;

        public setStart(start: Date) {
            this.start = start;
        }

        public setEnd(end: Date) {
            this.end = end;
        }

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter, this.start, this.end).then(result => {
                this.appointments = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize; 
            });
        } 

        public changePageSize(size: number) {
            this.pageSize = size;
            this.loadPage();
        }
    }
}