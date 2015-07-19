///<reference path="../common/ModalControllerBase.ts"/>
///<reference path="../Services/DoctorApiService.ts"/>

module Telemedicine {
    export class AppointmentDialogController extends ItemModalViewModel<IDoctor> {
        constructor(
            $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            item: IDoctor) {
            super($modalInstance, item);
        }
    }
}