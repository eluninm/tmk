///<reference path="../Services/PatientApiService.ts"/>

module Telemedicine {
    export class PatientPaymentController {
        static $inject = ["patientApiService", "balanceApiService", "$element","$scope"];
        private patientId: number;
        constructor(private patientApiService: PatientApiService,
            private balanceApiService: BalanceApiService,
            private $element: ng.IAugmentedJQuery,
            private $scope:any) {

            this.patientId = parseInt($element.data("id"));

            this.loadPage();
            this.loadBalance();

        }

        public payments: Array<IPaymentHistory>;

        public paymentValue: number;

        public balance: number;
        public totalCount: number;
        public currentPage: number;
        public pageSize: number;

        public loadPage(pageToLoad?: number) {
            var page = pageToLoad || this.currentPage; 
            this.patientApiService.getPaymentHistory(this.patientId, page, this.pageSize).then(result => {
                this.payments = result.Data;
                this.totalCount = result.TotalCount;
                this.currentPage = result.Page;
                this.pageSize = result.PageSize;
            });
        }

        public replenish() {
            if (this.paymentValue && this.paymentValue > 0) {
                this.balanceApiService.replenish(this.paymentValue).then(result => {
                    this.loadBalance();
                    this.loadPage();
                    this.paymentValue = null;
                    
                });


                
            }
        }

        public loadBalance() {
            this.balanceApiService.balance().then(result => {
                this.balance = result;
                this.$scope.$emit('child', this.balance);
            });
        }
    }
}
 