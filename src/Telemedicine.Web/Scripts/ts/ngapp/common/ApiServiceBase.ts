module Telemedicine {
    export interface IApiServiceBase<TModel> {
        getItems(): ng.IPromise<Array<TModel>>;
        getItem(itemId: number): ng.IPromise<TModel>;
    }

    export class ApiServiceBase<TModel> implements IApiServiceBase<TModel> {
        constructor(protected  baseUrl: string,
                    protected  urlResolverService: IUrlResolverService,
                    protected  $http: ng.IHttpService){}

        public getItems(): ng.IPromise<TModel[]> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl);
            return this.$http.get(url).then(result => result.data);
        }

        public getItem(itemId: number): ng.IPromise<TModel> {
            var url = this.urlResolverService.resolveUrl(this.baseUrl + "/" + itemId);
            return this.$http.get(url).then(result => result.data);
        }
    }
}