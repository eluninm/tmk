module Telemedicine {
    export class BalanceController{
        constructor(private balanceApiService: BalanceApiService,
            $modalInstance: ng.ui.bootstrap.IModalServiceInstance,
            item: IDoctor) { 
        }

        public debitingValue: number;

        public debiting(amount: number) {
            this.balanceApiService.debiting(amount).then(result => {
                //this. = result;
            });
        }
    }
}