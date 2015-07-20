///<reference path="../common/ModalControllerBase.ts"/>

module Telemedicine {
    export class PaymentDialogController extends ItemModalViewModel<IDoctor> {
        constructor(
            $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            item: IDoctor) {
            super($modalInstance, item);
        }
    }
}