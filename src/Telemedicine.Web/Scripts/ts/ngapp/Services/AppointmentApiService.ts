///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IAppointmentApiService {
        createItem(item: IAppointment, config?: ng.IRequestConfig);
    }

    export class AppointmentApiService implements IAppointmentApiService {
        private baseUrl = "/api/v1/appointment";
        private timeout: number = 10000;

        constructor(private $http: ng.IHttpService,
            private urlResolverService: UrlResolverService) {
        }

        createItem(item: IAppointment, config?: ng.IRequestConfig) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            this.$http.post(url, item, config || { timeout: this.timeout }).then(result => result.data);
        }
    }
}