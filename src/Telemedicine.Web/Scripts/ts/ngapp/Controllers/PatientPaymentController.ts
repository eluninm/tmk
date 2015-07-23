///<reference path="../Services/PatientApiService.ts"/>

module Telemedicine {
    export class PatientPaymentController {
        static $inject = ["patientApiService", "balanceApiService", "$scope"];

        constructor(private patientApiService: PatientApiService,
            private balanceApiService: BalanceApiService,
            private math: Math,
            private $scope: any) {
            this.loadPage(1);
            this.getBalance();
        }

        public paymentsHistory: Array<IPaymentHistory>;
        public paymentValue: number;
        public balance: number;
        public totalCount: number;
        public currentPage: number;
        public pageSize: number;

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage; 
            this.patientApiService.patientPaymentHistory(page, this.pageSize).then(result => {
                this.paymentsHistory = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize;
            });
        }

        public replenish(amount: number) {
            this.balanceApiService.replenish(amount).then(result => {
                this.getBalance();
            });
        }

        public getBalance() {
            this.balanceApiService.balance().then(result => {
                this.balance = result;
            });
        }


        public getPreviousPage(): number {
           /* var totalPages = this.totalCount / this.pageSize;
            var roundedTotalPages = this.math.round(totalPages);
            if ((totalPages - roundedTotalPages) != 0.0) {
                
            }*/
            return this.currentPage - 1;
        }

        public getNextPage(): number {
            return this.currentPage - 1;
        }


        public previousPage() {
            this.loadPage(this.currentPage - 1);
        }

        public nextPage() {
            this.loadPage(this.currentPage + 1);
            console.log("nextPage");
        }
         
    }
}
 