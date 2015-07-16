///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IRecommendationApiService extends IApiServiceBase<Recommendation> {
    }

    export class RecommendationApiService extends ApiServiceBase<Recommendation> implements IRecommendationApiService {
        static $inject = ["$http", "urlResolverService"];
        constructor($http: ng.IHttpService,
            urlResolverService: UrlResolverService) {
            super("~/api/v1/recommendations", urlResolverService, $http);
        }
    }
}