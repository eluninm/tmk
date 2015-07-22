module Telemedicine {
    export interface IAppointment {
        DoctorId: number;
        AppointmentDate: Date;
    }

    export enum AppointmentStatus {
        Declined,
        Closed
    } 
}