///<reference path="../common/ModalControllerBase.ts"/>
///<reference path="../../dts/bootstrap.v3.datetimepicker.d.ts"/>


interface DatetimepickerOptions {
    stepping: number;
}

interface Datetimepicker {
    date(): Date;
}

module Telemedicine {
    export class AppointmentDialogController extends ItemModalViewModel<IDoctor> {
        constructor(
            $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            item: IDoctor,
            public appointment: IAppointment) {
            super($modalInstance, item);

            this.minDate = new Date();
        }

        public initDatetimepicker() {
            $("#datetimepicker1").datetimepicker({
                inline: true,
                sideBySide: true,
                locale: 'ru',
                minDate: this.minDate,
                icons: {
                    up: "glyphicon glyphicon-triangle-top",
                    down: "glyphicon glyphicon-triangle-bottom",
                    next: "glyphicon glyphicon-triangle-right",
                    previous: "glyphicon glyphicon-triangle-left"
                }
            });
        }

        public book() {
            this.appointment.AppointmentDate = new Date();
            this.ok("appointment");
        }

        public minDate: Date;
        public maxDate: Date;
        public appointmentDate: Date;
        public appointmentTime: any;
    }
}