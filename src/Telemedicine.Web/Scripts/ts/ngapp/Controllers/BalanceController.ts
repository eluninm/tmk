module Telemedicine {
    export class BalanceController{
        static $inject = ["balanceApiService",  "$scope"];
        constructor(private balanceApiService: BalanceApiService) {
            console.log("constructor()"); 
        }

        public debitValue: number;

        public debit(amount: number) {
            console.log("debit()");
            this.balanceApiService.debit(amount).then(result => {
                //this. = result;
            });
        }
    }
}