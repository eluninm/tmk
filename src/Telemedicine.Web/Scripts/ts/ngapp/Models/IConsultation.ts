module Telemedicine {
    export interface IConsultation {
        Id: number;
        Created: Date;
        DoctorTitle: string;
        DoctorSpecialization: string;
    }
}