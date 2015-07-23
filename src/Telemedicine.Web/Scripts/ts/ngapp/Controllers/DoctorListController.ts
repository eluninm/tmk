///<reference path="../Services/DoctorApiService.ts"/>
///<reference path="../Services/SpecializationApiService.ts"/>
///<reference path="../Services/AppointmentApiService.ts"/>
///<reference path="../SignalR/SignalR.ts"/>

module Telemedicine {
    export class DoctorListController {
        static $inject = ["doctorApiService", "specializationApiService", "appointmentApiService", "$modal", "$scope"];

        constructor(private doctorApiService: DoctorApiService,
            private specializationApiService: SpecializationApiService,
            private appointmentApiService: IAppointmentApiService,
            private $modal: ng.ui.bootstrap.IModalService,
            private $scope: any) {
            this.loadPage();
            this.loadSpecializations();
            this.initialize();
        }

        public doctors: Array<IDoctor>;
        public specializations: Array<ISpecialization>;
        public selectedSpecialization: ISpecialization;
        public doctorTitleFilter: string;

        public totalCount: number;
        public currentPage: number = 1;
        public pageSize: number = 1;

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            var sid = this.selectedSpecialization ? this.selectedSpecialization.Id : 0;
            this.doctorApiService.getDoctorList(page, this.pageSize, this.doctorTitleFilter, sid).then(result => {
                this.doctors = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize;
            });
        }

        public loadSpecializations() {
            this.specializationApiService.getItems().then(result => {
                this.specializations = result;
            });
        }

        public selectSpecialization(specialization: ISpecialization) {
            this.selectedSpecialization = specialization;
            this.loadPage();
        }

        public changePageSize(size: number) {
            this.pageSize = size;
            this.loadPage();
        }

        public openDetailsDialog(doctor: IDoctor) {
            this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/doctorDetails.html",
                controller: "DoctorDetailsController as viewModel",
                windowClass: "infomodaldialog",
                resolve: {
                    item: () => doctor
                }
            });
        }

        public openAppointmentDialog(doctor: IDoctor) {
            var appointment: IAppointment = { DoctorId: doctor.Id, AppointmentDate: new Date() };
            this.doctorApiService.getDoctorTimeWindows(doctor.Id).then(result => {
                var appointmentDialog = this.$modal.open({
                    templateUrl: "/Content/tmpls/dialogs/appointmentDialog.html",
                    controller: "AppointmentDialogController as viewModel",
                    windowClass: "appointmentmodaldialog",
                    resolve: {
                        item: () => doctor,
                        appointment: () => appointment,
                        timeWindows: () => result
                    }
                });

                appointmentDialog.result.then((result) => {
                    if (result === "appointment") {
                        this.appointmentApiService.createItem(appointment);
                        appointmentDialog.close();
                        this.openPaymentDialog();
                    }
                });
            });
        }

        public openPaymentDialog() {
            var paymentDialog = this.$modal.open({
                templateUrl: "/Content/tmpls/dialogs/paymentDialog.html",
                controller: "PaymentDialogController as viewModel",
                windowClass: "paymentmodaldialog",
                resolve: {
                    item: () => null
                }
            });
        }

        initialize() {
            var signal = $.connection.signalHub;
            signal.client.onDoctorUpdated = (s) => this.onDoctorUpdated(s);
        }

        onDoctorUpdated(doctor: IDoctor) {
            for (var i = 0; i < this.doctors.length; i++) {
                if (this.doctors[i].Id == doctor.Id) {
                    this.$scope.$apply(() => {
                        this.doctors[i] = doctor;
                    });
                }
            }
        }
    }
}