﻿///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/> 
///<reference path="../../dts/moment.d.ts"/>

module Telemedicine {
    export interface IDoctorApiService {
        getDoctorList(page?: number, pageSize?: number, title?: string, specializationId?: number): ng.IPromise<IPagedList<IDoctor>>;
        getDoctorDetails(doctorId: number): ng.IPromise<string>;
        getDoctorAppointments(doctorId: number, page?: number, pageSize?: number, patientTitleFilter?: string): ng.IPromise<IPagedList<IDoctorAppointment>>;
        getDoctorTimeWindows(doctorId: number): ng.IPromise<IDoctorTimeWindows>;
        getPaymentHistory(doctorId: number, page?: number, pageSize?: number): ng.IPromise<IPagedList<IPaymentHistory>>;
        getPaymentHistory(doctorId: number, page?: number, pageSize?: number): ng.IPromise<IPagedList<IPaymentHistory>>;
        getDoctorTimelineByMonth(doctorId: number, year: number, month: number): ng.IPromise<Array<ITimelineDate>>;
        statusIsAvailable(): ng.IPromise<boolean>;
        decline(id: number): ng.IPromise<void>;
    }

    export class DoctorApiService implements IDoctorApiService {
        private baseUrl = "/api/v1/doctor";

        constructor(private $http: ng.IHttpService,
            private urlResolverService: UrlResolverService) {
        }

        getDoctorList(page?: number, pageSize?: number, title?: string, specializationId?: number): ng.IPromise<IPagedList<IDoctor>> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            var query: any = {}, querySeparator = "?";

            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            if (title) {
                query.titleFilter = title;
            }
            if (specializationId) {
                query.specializationIdFilter = specializationId;
            }

            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }

            return this.$http.get(url).then(result => result.data);
        }

        getDoctorDetails(doctorId: number): angular.IPromise<string> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/details");
            return this.$http.get(url).then(result => result.data);
        }

        getDoctorAppointments(doctorId: number, page?: number, pageSize?: number, patientTitleFilter?: string, start?: Date, end?: Date,
            needDeclined?: boolean, needReady?: boolean, needClosed?: boolean, filter?: string): angular.IPromise<IPagedList<any>> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/appointments");
            var query: any = {}, querySeparator = "?";

            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }
            if (patientTitleFilter) {
                query.patientTitleFilter = patientTitleFilter;
            }

            if (start)
            {
                query.start = moment(start).utc().toISOString();
            }
            if (end)
            {
                query.end = moment(end).utc().toISOString();
            }

            if (needDeclined) {
                query.needDeclined = needDeclined; 
            }

            if (needReady) {
                query.needReady = needReady;
            }

            if (needClosed) {
                query.needClosed = needClosed;
            }

            if (filter) {
                query.filter = filter;
            }

            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }

            return this.$http.get(url).then(result => result.data);
        }

        getDoctorTimeWindows(doctorId: number): ng.IPromise<IDoctorTimeWindows> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/timeWindows");
            return this.$http.get(url).then(result => result.data);
        }

        getPaymentHistory(doctorId: number, page?: number, pageSize?: number, start?: Date, end?:Date): angular.IPromise<IPagedList<IPaymentHistory>> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/paymentHistory");

            var query: any = {}, querySeparator = "?";

            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
            }

            if (start && end) {
                query.start = start.toISOString();
                query.end = end.toISOString();
            }

            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }

            return this.$http.get(url).then(result => result.data);
        }

        getDoctorTimelineByMonth(doctorId: number, year: number, month: number): angular.IPromise<ITimelineDate[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/timeline/" + year + "/" + month);
            return this.$http.get(url).then(result => result.data);
        }

        changeDoctorStatus(isAvailable: boolean) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + isAvailable + "/changeStatus");
            return this.$http.post(url, { "isAvailable": isAvailable }).then(result => result.data);
        }

        changeHourStatus(date: Date, selectedHour: number, status: TimelineHourType) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/changeHourStatus/" + date.getFullYear() + "/" +
                (date.getMonth() + 1) + "/" +
                date.getDate() + "/" +
                selectedHour + "/" +
                status);
            return this.$http.post(url, {
                "year": date.getFullYear(),
                "month": (date.getMonth() + 1),
                "day": date.getDate(),
                "selectedHour": selectedHour,
                "status": status
            }).then(result => result.data);
        }

        statusIsAvailable(): ng.IPromise<boolean> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/statusIsAvailable");
            return this.$http.get(url).then(result => result.data);
        }

        decline(id: number): ng.IPromise<void> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/decline/" + id);
            return this.$http.post(url, { 'id': id }).then(result => {});
        }
    }
}