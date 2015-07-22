///<reference path="../Services/BalanceApiService.ts"/>

module Telemedicine {
    export class BalanceController{
        static $inject = ["balanceApiService",  "$scope"];
        constructor(private balanceApiService: BalanceApiService) { 
        }

        public debitValue: number;

        public debit(amount: number) {
            console.log("debit()");
              this.balanceApiService.debit(amount).then(result => {
                  console.log(result);
              }); 
        }
    }
}


 