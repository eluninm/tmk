///<reference path="../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class BalanceController{
        static $inject = ["balanceApiService",  "$scope"];
        constructor(private balanceApiService: BalanceApiService, private $scope: any) {
            this.getBalance();
        }

        public debitValue: number;
        public balance: number;

        public debit(amount: number) { 
            this.balanceApiService.debit(amount).then(result => { 
                this.getBalance();
              }); 
        }

        public replenish(amount: number) { 
            this.balanceApiService.replenish(amount).then(result => {
                this.debitValue = 0;
                this.$scope.$emit('ReplenishSuccess', result);
                this.getBalance();
            }); 
        }


        public getBalance() {
            this.balanceApiService.balance().then(result => {
                this.balance = result; 
            }); 
        }
    }
}


 