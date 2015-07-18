///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IConsultationApiService {
        consultationMessages(consultationId: string): ng.IPromise<Array<IConsultationMessage>>;
    }

    export class ConsultationApiService implements IConsultationApiService {
        private baseUrl = "/api/v1/consultation";

        constructor(private $http: ng.IHttpService,
                    private urlResolverService: UrlResolverService) {
        }

        consultationMessages(consultationId: string): ng.IPromise<IConsultationMessage[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + consultationId + "/messages");
            return this.$http.get(url).then(result => result.data);
        }
    }
}