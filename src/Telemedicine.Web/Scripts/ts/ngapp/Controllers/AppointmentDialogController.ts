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
            public appointment: IAppointment,
            private timeWindows: IDoctorTimeWindows) {
            super($modalInstance, item);
        }

        public initDatetimepicker() {
            $("#datetimepicker1").datetimepicker({
                inline: true,
                sideBySide: true,
                locale: 'ru',
                minDate: this.timeWindows.MinDate,
                maxDate: this.timeWindows.MaxDate,
                stepping: this.timeWindows.WindowSize,
                defaultDate: this.timeWindows.NearestAvailable.toString(),
                icons: {
                    up: "glyphicon glyphicon-triangle-top",
                    down: "glyphicon glyphicon-triangle-bottom",
                    next: "glyphicon glyphicon-triangle-right",
                    previous: "glyphicon glyphicon-triangle-left"
                }
            });
        }

        public book() {
            this.appointment.AppointmentDate = $("#datetimepicker1").datetimepicker().data("date");
            this.ok("appointment");
        }

        public appointmentDate: Date;
        public appointmentTime: any;
    }
}