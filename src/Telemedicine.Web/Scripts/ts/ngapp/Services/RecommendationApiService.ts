///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IRecommendationApiService extends IApiServiceBase<IRecommendation> {
        addRecommendation(patientId: number, text:string);
    }

    export class RecommendationApiService extends ApiServiceBase<IRecommendation> implements IRecommendationApiService {
        static $inject = ["$http", "urlResolverService"];
        constructor($http: ng.IHttpService,
            urlResolverService: UrlResolverService) {
            super("~/api/v1/recommendation", urlResolverService, $http);
        }

        addRecommendation(patientId: number, recommendationText: string) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/addRecommendation/" + patientId + "/" + recommendationText);
            return this.$http.post(url, { patientId: patientId, recommendationText: recommendationText }).then(result => result.data);
        }


    }
}