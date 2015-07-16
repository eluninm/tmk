///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IPatientApiService {
        patientRecommendations(patientId: string): ng.IPromise<Array<Recommendation>>;
    }

    export class PatientApiService implements IPatientApiService {
        private baseUrl = "/api/v1/patients";

        constructor(private $http: ng.IHttpService,
                    private urlResolverService: UrlResolverService) {
        }

        public patientRecommendations(patientId: string): ng.IPromise<Recommendation[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/recommendations");
            return this.$http.get(url).then(result => result.data); 
        }
    }
}