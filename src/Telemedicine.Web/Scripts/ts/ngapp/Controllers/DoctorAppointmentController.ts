///<reference path="../Services/DoctorApiService.ts"/>
///<reference path="../../dts/moment.d.ts"/>

module Telemedicine {
    export class DoctorAppointmentController {
        static $inject = ["doctorApiService", "$modal", "$element","$scope"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService,
            private $modal: ng.ui.bootstrap.IModalService,
            private $element: ng.IAugmentedJQuery,
            private $scope:any) {
            this.doctorId = parseInt($element.attr("data-id"));
            this.filter = "All";
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
        public filter: string; 

        needDeclined: boolean = true;
        needReady: boolean = true;
        needClosed: boolean = true; 

        public setStart(start: Date) {
            this.start = start;
        }

        public setEnd(end: Date) {
            this.end = end;
        }

        public setFilter(filter: string) {
            this.filter = filter;
        }

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getDoctorAppointments(this.doctorId, page, this.pageSize, this.patientTitleFilter,
                this.start, this.end, null, null, null, this.filter).then(result => {
                    this.appointments = result.Data;
                    this.totalCount = result.TotalCount;
                    this.currentPage = result.Page;
                    this.pageSize = result.PageSize;
                 
                });
        } 

        public filterStatusEqualsAll() {
            this.needDeclined = true;
            this.needReady = true;
            this.needClosed = true;
            this.loadPage();
        }

        public filterStatusEqualsReady() {
            this.needDeclined = false;
            this.needReady = true;
            this.needClosed = false; 
            this.loadPage(); 
        }

        public filterStatusExcludingReady() {
            this.needDeclined = true;
            this.needReady = false;
            this.needClosed = true;
            this.loadPage(); 
        }

        public getStatusText(status: AppointmentStatus) {
            console.log(status);
            switch (status) {
                case AppointmentStatus.Ready:
                {
                    return "Отмена";
                }
                case AppointmentStatus.Closed:
                {
                    return "Консультация завершена";
                }
                case AppointmentStatus.Declined:
                {
                    return "Не состоялась";
                }
            }
        }

        public changePageSize(size: number) {
            this.pageSize = size;
            this.loadPage();
        }

        public decline(id: number) {
            this.doctorApiService.decline(id).then(result => {
                this.loadPage();
            });
        }
    }
}