module Telemedicine {
    export interface IAppointmentServer { 
        newAppointment(doctorId: string): JQueryPromise<void>; 
    }
}