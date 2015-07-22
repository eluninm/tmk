module Telemedicine {
    export interface IBalanceApiService {
        debiting(amount: number): angular.IPromise<string>;
    }

    export class BalanceApiService implements IBalanceApiService {
        private baseUrl = "/api/v1/balance";

        constructor(private $http: ng.IHttpService,
            private urlResolverService: UrlResolverService) {
        }


        public debitValue = 0;

        debiting(amount: number): angular.IPromise<string> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/debit/" + amount );
            return this.$http.get(url).then(result => result.data);
        }
    }
}