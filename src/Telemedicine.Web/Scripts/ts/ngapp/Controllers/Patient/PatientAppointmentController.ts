///<reference path="../../Services/PatientApiService.ts"/>

module Telemedicine {
    export class PatientAppointmentController {
        static $inject = ["patientApiService", "$modal", "$element"];
        private patientId: number;

        constructor(private patientApiService: PatientApiService,
            private $modal: ng.ui.bootstrap.IModalService,
            private $element: ng.IAugmentedJQuery) {
            this.patientId = parseInt($element.attr("data-id"));
            this.loadPage();
        }

        public appointments: Array<IPatientAppointment>;

        public totalCount: number;
        public currentPage: number = 1;
        public pageSize: number = 1;

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            this.patientApiService.getPatientAppointments(this.patientId, page, this.pageSize).then(result => {
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