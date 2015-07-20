///<reference path="../common/ModalControllerBase.ts"/>

module Telemedicine {
    export class AppointmentDialogController extends ItemModalViewModel<IDoctor> {
        constructor(
            $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            item: IDoctor) {
            super($modalInstance, item);

            this.minDate = new Date();
        }

        public initDatetimepicker() {
            $("#datetimepicker1").datetimepicker({
                inline: true,
                sideBySide: true,
                locale: 'ru',
                icons: {
                    up: "glyphicon glyphicon-triangle-top",
                    down: "glyphicon glyphicon-triangle-bottom",
                    next: "glyphicon glyphicon-triangle-right",
                    previous: "glyphicon glyphicon-triangle-left"
                }
            });
        }

        public minDate: Date;
        public maxDate: Date;
        public appointmentDate: Date;
        public appointmentTime: any;
    }
}