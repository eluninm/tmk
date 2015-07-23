///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IPatientApiService {
        patientRecommendations(patientId: string): ng.IPromise<Array<IRecommendation>>;
        patientConsultations(patientId: string): ng.IPromise<Array<IConsultation>>;
        patientPaymentHistory(): ng.IPromise<Array<IPaymentHistory>>;
        getPatientAppointments(patientId: number, page?: number, pageSize?: number): ng.IPromise<IPagedList<IPatientAppointment>>;
    }

    export class PatientApiService implements IPatientApiService {
        private baseUrl = "/api/v1/patient";

        constructor(private $http: ng.IHttpService,
                    private urlResolverService: UrlResolverService) {
        }

        public patientRecommendations(patientId: string): ng.IPromise<IRecommendation[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/recommendations");
            return this.$http.get(url).then(result => result.data);
        }

        public patientConsultations(patientId: string): ng.IPromise<IConsultation[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/consultations");
            return this.$http.get(url).then(result => result.data);
        }


        public patientPaymentHistory(): ng.IPromise<Array<IPaymentHistory>> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/PaymentsHistory");
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
    }
}