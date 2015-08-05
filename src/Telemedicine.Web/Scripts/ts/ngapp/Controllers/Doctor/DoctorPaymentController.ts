﻿///<reference path="../../Services/PatientApiService.ts"/>
///<reference path="../../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class DoctorPaymentController {
        static $inject = ["doctorApiService", "balanceApiService", "$element"];
        private doctorId: number;

        constructor(private doctorApiService: DoctorApiService,
            private balanceApiService: BalanceApiService,
            private $element: ng.IAugmentedJQuery) {

            this.doctorId = parseInt($element.data("id"));

            this.loadPage();
            this.loadBalance();
        }

        public payments: Array<IPaymentHistory>;
        public debitValue: number;

        public balance: number;

        public totalCount: number;
        public currentPage: number;
        public pageSize: number;
        public start: Date;
        public end: Date;

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            this.doctorApiService.getPaymentHistory(this.doctorId, page, this.pageSize, this.start, this.end).then(result => {
                this.payments = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize;
            });
        }

        public debit() {
            if (this.debitValue && this.debitValue > 0) {
                this.balanceApiService.debit(this.debitValue).then(result => {
                    this.loadBalance();
                    this.loadPage();
                    this.debitValue = null;
                });
            }
        }


        public setStart(start: Date) {
            this.start = start;
        }

        public setEnd(end: Date) {
            this.end = end;
        }


        public loadBalance() {
            this.balanceApiService.balance().then(result => {
                this.balance = result;
            });
        }
    }
}
 