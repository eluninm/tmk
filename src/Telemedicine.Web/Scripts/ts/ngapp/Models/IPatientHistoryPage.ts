module Telemedicine {
    export interface IPatientHistoryPage {
        PaymentItems: Array<IPaymentHistory>;
        TotalCount: number; 
    }
}