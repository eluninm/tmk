///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IPatientApiService {
        patientRecommendations(patientId: string): ng.IPromise<Array<IRecommendation>>;
        patientConsultations(patientId: string): ng.IPromise<Array<IConsultation>>; 
        getPatientAppointments(patientId: number, page?: number, pageSize?: number): ng.IPromise<IPagedList<IPatientAppointment>>;
        getPaymentHistory(patientId: number, page: number, pageSize: number): ng.IPromise<IPagedList<IPaymentHistory>>;
        payConsultation(doctorId: number, value: number): ng.IPromise<Boolean>;
}

    export class PatientApiService implements IPatientApiService {
        private baseUrl = "/api/v1/patient";

        constructor(private $http: ng.IHttpService,
            private urlResolverService: UrlResolverService,
            private $rootScope: ng.IRootScopeService) {
        }

        public paymentsHistory: Array<IPaymentHistory>;

        public patientRecommendations(patientId: string, doctorId?: number): ng.IPromise<IRecommendation[]> {
            var query: any = {}, querySeparator = "?";
            var url = this.baseUrl + "/" + patientId + "/recommendations";
            query.patientId = patientId;
            if (doctorId) {
                query.doctorId = doctorId;
            }
             

            for (var key in query) {
                if (query.hasOwnProperty(key)) {
                    url += querySeparator + key + "=" + encodeURIComponent(query[key]);
                    if (querySeparator === "?") {
                        querySeparator = "&";
                    }
                }
            }

            //var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/recommendations");
            return this.$http.get(url).then(result => result.data);
        }

        public patientConsultations(patientId: string): ng.IPromise<IConsultation[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/consultations");
            return this.$http.get(url).then(result => result.data);
        }

        public getPaymentHistory(patientId: number, page: number, pageSize: number): ng.IPromise<IPagedList<IPaymentHistory>> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/paymentHistory");

            var query: any = {}, querySeparator = "?";

            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
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

        getPatientAppointments(patientId: number, page?: number, pageSize?: number): ng.IPromise<IPagedList<IPatientAppointment>> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/appointments");
            var query: any = {}, querySeparator = "?";

            if (page) {
                query.page = page;
            }
            if (pageSize) {
                query.pageSize = pageSize;
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

        
        public payConsultation(doctorId: number, value: number): ng.IPromise<Boolean> {
            var querySeparator = "/";
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/payConsultation/" + encodeURIComponent(String(doctorId)) + querySeparator + encodeURIComponent(String(value)) );
            return this.$http.post(url, { "doctorId": doctorId, "value": value }).then(result => result.data);
        }
    }
}