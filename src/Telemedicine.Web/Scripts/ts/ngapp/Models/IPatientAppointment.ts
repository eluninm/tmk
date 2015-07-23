module Telemedicine {
    export interface IPatientAppointment {
        Id: number;
        DoctorId: number;
        DoctorTitle: string;
        DoctorSpecialization: string;
        Date: Date;
        ConsultationId: string;
        Status: AppointmentStatus;
    }
}