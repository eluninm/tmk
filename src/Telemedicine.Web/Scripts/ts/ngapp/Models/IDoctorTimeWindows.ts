module Telemedicine {
    export interface IDoctorTimeWindows {
        DoctorId: number;
        WindowSize: number;
        MinDate: Date;
        MaxDate: Date;
        NearestAvailable: Date;
        Enabled: Array<Date>;
        Disabled: Array<Date>;
    }
}