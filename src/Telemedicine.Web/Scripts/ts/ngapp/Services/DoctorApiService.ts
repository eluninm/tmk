///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IDoctorApiService {
        getDoctorList(): ng.IPromise<Array<IDoctor>>;
        getDoctorDetails(doctorId: number): ng.IPromise<string>;
    }

    export class DoctorApiService implements IDoctorApiService {
        private baseUrl = "/api/v1/doctor";

        constructor(private $http: ng.IHttpService,
            private urlResolverService: UrlResolverService) {
        }

        getDoctorList(): ng.IPromise<IDoctor[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            return this.$http.get(url).then(result => result.data);
        }

        getDoctorDetails(doctorId: number): angular.IPromise<string> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + doctorId + "/details");
            return this.$http.get(url).then(result => result.data);
        }
    }
}