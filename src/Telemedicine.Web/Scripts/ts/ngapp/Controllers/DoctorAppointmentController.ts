///<reference path="../Services/DoctorApiService.ts"/>
///<reference path="../../dts/moment.d.ts"/>

module Telemedicine {
    export class DoctorAppointmentController {
        static $inject = ["doctorApiService", "$modal", "$element"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService,
            private $modal: ng.ui.bootstrap.IModalService,
            private $element: ng.IAugmentedJQuery) {
            this.doctorId = parseInt($element.attr("data-id"));

            if (location.hash) {
                var parameters = this.parseUrlQuery();
                this.start = moment(parameters["start"]).toDate();
                this.end = moment(parameters["end"]).toDate();
            }
        }

        public parseUrlQuery(): Array<Object> {
            var data = new Array();
            if (location.hash) {
                var pair = (location.hash.substr(1)).split('&');
                for (var i = 0; i < pair.length; i++) {
                    var param = pair[i].split('=');
                    data[param[0]] = param[1];
                }
            }
            return data;
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
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter,
                this.start, this.end,false,true,true).then(result => {
                this.appointments = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize; 
            });
        } 


        public loadAppointmentPageWithStatusEqualsReady(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter,
                this.start, this.end, false, true, false).then(result => {
                this.appointments = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize;
            });
        } 

        public loadAppointmentPageWithStatusEqualsClosed(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter,
                this.start, this.end, false, false, true).then(result => {
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