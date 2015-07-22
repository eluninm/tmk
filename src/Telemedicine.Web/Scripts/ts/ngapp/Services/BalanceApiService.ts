///<reference path="../common/ApiServiceBase.ts"/>
///<reference path="../common/UrlResolverService.ts"/>
 
 
module Telemedicine {
    export interface IBalanceApiService {
        debit(amount: number): angular.IPromise<string>; 
        replenish (amount: number);
    }

    export class BalanceApiService implements IBalanceApiService {
        private baseUrl = "/api/v1/balance";

        constructor(private $http: ng.IHttpService,
            private urlResolverService: UrlResolverService) {
        }

        public debit(amount: number): angular.IPromise<string> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/debit/" + amount);
            return this.$http.post(url, null).then(result => result.data);
        }

        public replenish (amount: number) {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/replenish/" + amount);
            return this.$http.post(url, null).then(result => result.data);
        }
    }
} 