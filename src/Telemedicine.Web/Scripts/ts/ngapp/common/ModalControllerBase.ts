/// <reference path="../../dts/angular-ui-bootstrap.d.ts" />

module Telemedicine {
    export interface IItemModalViewModel<TModel> {
        item: TModel;
        ok();
        cancel();
    }

    export class ItemModalViewModel<TModel> implements IItemModalViewModel<TModel> {
        constructor(private $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            public item: TModel) {
        }

        public ok() {
            this.$modalInstance.close(true);
        }

        public cancel() {
            this.$modalInstance.dismiss("cancel");
        }
    }
}