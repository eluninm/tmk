///<reference path="../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class BalanceController{
        static $inject = ["balanceApiService",  "$scope"];
        constructor(private balanceApiService: BalanceApiService) { 
        }

        public debitValue: number;

        public debit(amount: number) { 
              this.balanceApiService.debit(amount).then(result => { 
              }); 
        }

        public replenish(amount: number) { 
            this.balanceApiService.replenish(amount).then(result => { 
              }); 
        }
    }
}


 