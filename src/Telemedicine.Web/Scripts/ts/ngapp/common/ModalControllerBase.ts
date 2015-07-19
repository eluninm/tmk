/// <reference path="../../dts/angular-ui-bootstrap.d.ts" />

module Telemedicine {
    export interface IItemModalViewModel<TModel> {
        item: TModel;
        ok(result?: any);
        cancel();
    }

    export class ItemModalViewModel<TModel> implements IItemModalViewModel<TModel> {
        constructor(private $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            public item: TModel) {
        }

        public ok(result: string) {
            this.$modalInstance.close(result);
        }

        public cancel() {
            this.$modalInstance.dismiss("cancel");
        }
    }
}