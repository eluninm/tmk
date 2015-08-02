///<reference path="../../Services/DoctorApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class DoctorTimelineController {
        static $inject = ["doctorApiService", "balanceApiService", "$element"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService,
            private balanceApiService: BalanceApiService,
            private $element: ng.IAugmentedJQuery) {

            this.doctorId = parseInt($element.data("id"));

            this.loadTimeline();
            this.loadBalance();
        }

        public timeLineDates: Array<ITimelineDate>;
        public balance: number;
        public year: number;
        public month: number;
            
        public loadTimeline() {
            this.year = 2015;
            this.month = 8;
            this.doctorApiService.getDoctorTimelineByMonth(this.doctorId, this.year, this.month).then(result => {
                this.timeLineDates = result;
            });
        }

        public loadBalance() {
            this.balanceApiService.balance().then(result => {
                this.balance = result;
            });
        }
    }
}