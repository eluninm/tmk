module Telemedicine {
    export interface IRecommendationApiService extends IApiServiceBase<Recommendation> {
    }

    export class RecommendationApiService extends ApiServiceBase<Recommendation> implements IRecommendationApiService {
        constructor($http: ng.IHttpService,
            urlResolverService: IUrlResolverService) {
            super("~/api/v1/recommendations", urlResolverService, $http);
        }
    }
}