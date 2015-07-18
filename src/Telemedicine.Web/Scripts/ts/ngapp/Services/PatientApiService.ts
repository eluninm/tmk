///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>

module Telemedicine {
    export interface IPatientApiService {
        patientRecommendations(patientId: string): ng.IPromise<Array<IRecommendation>>;
        patientConsultations(patientId: string): ng.IPromise<Array<IConsultation>>;
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
    }
}