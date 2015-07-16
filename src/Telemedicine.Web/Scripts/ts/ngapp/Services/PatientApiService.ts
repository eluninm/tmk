module Telemedicine {
    export interface IPatientApiService {
        patientRecommendations(patientId: number): ng.IPromise<Array<Recommendation>>;
    }

    export class PatientApiService implements IPatientApiService {
        private baseUrl = "/api/v1/patients";

        constructor(private $http: ng.IHttpService,
                    private urlResolverService: IUrlResolverService) {
        }

        public patientRecommendations(patientId: number): ng.IPromise<Recommendation[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + patientId + "/recommendations");
            return this.$http.get(url).then(result => result.data); 
        }
    }
}