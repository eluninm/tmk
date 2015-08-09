///<reference path="../../Services/PatientApiService.ts"/>
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

        public allPayments: Array<IPaymentHistory>;
        public payments: Array<IPaymentHistory>;
        public debitValue: number;

        public balance: number;

        public totalBalance: number;
        public totalCount: number;
        public currentPage: number = 1;
        public pageSize: number = 10;
        public start: Date;
        public end: Date;

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage;
            var currentPageSize = this.pageSize;

            if (typeof(this.allPayments) == 'undefined') {
                this.doctorApiService.getPaymentHistory(this.doctorId, null, null, this.start, this.end).then(result => {
                    this.allPayments = result.Data;
                    this.payments = this.allPayments.slice((page - 1) * currentPageSize, page * currentPageSize);
                    this.totalCount = result.TotalCount;
                    this.currentPage = page;

                    this.totalBalance = 0;

                    for (var i in this.allPayments)
                    {
                        this.totalBalance += this.allPayments[i].Value;
                    }
                });
            } else {
                this.payments = this.allPayments.slice((page - 1) * currentPageSize, page * currentPageSize);
                this.currentPage = page;
            }
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

        public changePageSize(size: number) {
            this.pageSize = size;
            this.loadPage(1);
        }
    }
}
 