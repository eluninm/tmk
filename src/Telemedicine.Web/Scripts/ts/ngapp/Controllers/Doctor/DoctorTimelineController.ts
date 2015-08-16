///<reference path="../../Services/DoctorApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/> 
///<reference path="../../../dts/moment.d.ts"/>
module Telemedicine {
    export class DoctorTimelineController {
        static $inject = ["doctorApiService", "balanceApiService", "$element", "$scope"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService,
            private balanceApiService: BalanceApiService,
            private $element: ng.IAugmentedJQuery,
            private $scope: any) {

            this.doctorId = parseInt($element.data("id"));

            this.loadTimeline();
            this.loadBalance();
        }

        public currentTimeLine: ITimelineDate;
        public timeLineDates: Array<ITimelineDate>;
        public balance: number;
        public year: number = 2015;
        public month: number = 8;

        private selectedHour: number;
        private curentDate: Date = new Date();

        public loadTimeline() {
            //this.year = 2015;
            //this.month = 8;
            this.doctorApiService.getDoctorTimelineByMonth(this.doctorId, this.year, this.month).then(result => {
                this.timeLineDates = result;
                this.updateTimeLine();
                document.getElementById("patientListOnCurrentDayLink").click();
            });
        }

        public loadBalance() {
            this.balanceApiService.balance().then(result => {
                this.balance = result;
            });
        }


        public selectHour(hour: number) {
            this.selectedHour = hour;
        }

        public isActiveCellHour(date) {
            var now = new Date();
            var cellDate = new Date(date.toString());
            return cellDate > now;
        }

        public availableHour() {
            this.doctorApiService.changeHourStatus(this.curentDate, this.selectedHour, TimelineHourType.Working).then(result => {
                this.loadTimeline();
            });
        }


        public unavailableHour() {
            this.doctorApiService.changeHourStatus(this.curentDate, this.selectedHour, TimelineHourType.NotWorking).then(result => {
                this.loadTimeline();
            });
        }

        public clearHour() {
            this.doctorApiService.changeHourStatus(this.curentDate, this.selectedHour, TimelineHourType.Clear).then(result => {
                this.loadTimeline();

            });
        }


        private updateTimeLine() {
            console.log("updateTimeLine");
            for (var i = 0; i < this.timeLineDates.length; i++) {

                var date = Number(this.timeLineDates[i].Date.toString().substr(8, 2));

                if (date == this.curentDate.getDate()) {
                    this.currentTimeLine = this.timeLineDates[i];
                    if (!this.$scope.$$phase) {
                        this.$scope.$apply();
                    }
                }
            }
        }

        public range(n: number) {
            return new Array(n);
        }

        public changeDate(date: Date) {
            this.curentDate = date;
            console.log("changeDate");
            if ((date.getMonth() + 1) != this.month || date.getFullYear() != this.year) {
                this.month = date.getMonth() + 1;
                this.year = date.getFullYear();
                this.loadTimeline();
                console.log("month is changed");
            } else {
                this.updateTimeLine();
            }
        }

        public showMyEvents() {
            var start = moment(new Date(this.curentDate.setHours(this.selectedHour))).utc().startOf('day');
            var end = moment(new Date(this.curentDate.setHours(this.selectedHour))).utc().endOf('day');
            window.location.href = "/Doctor/Home/MyEvents#start=" + start.toISOString() + "&end=" + end.toISOString();
        }
    }
}