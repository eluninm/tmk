///<reference path="../common/ModalControllerBase.ts"/>
///<reference path="../Services/DoctorApiService.ts"/>

module Telemedicine {
    export class DoctorDetailsController extends ItemModalViewModel<IDoctor> {
        constructor(private doctorApiService: DoctorApiService,
            $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            item: IDoctor) {
            super($modalInstance, item);
            this.loadDetails(item.Id);
        }

        public doctorDetails: string;

        public loadDetails(doctorId: number) {
            this.doctorApiService.getDoctorDetails(doctorId).then(result => {
                this.doctorDetails = result;
            });
        }
    }
}