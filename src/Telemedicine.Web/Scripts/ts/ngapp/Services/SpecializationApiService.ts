///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface ISpecializationApiService extends IApiServiceBase<ISpecialization> {
    }

    export class SpecializationApiService extends ApiServiceBase<ISpecialization> implements ISpecializationApiService {
        static $inject = ["$http", "urlResolverService"];
        constructor($http: ng.IHttpService,
            urlResolverService: UrlResolverService) {
            super("~/api/v1/specialization", urlResolverService, $http);
        }
    }
}