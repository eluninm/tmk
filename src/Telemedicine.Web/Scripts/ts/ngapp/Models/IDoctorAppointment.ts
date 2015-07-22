module Telemedicine {
    export interface IDoctorAppointment {
        Id: number;
        PatientId: number;
        PatientTitle: string;
        PatientAvatarUrl: string;
        Date: Date;
        ConsultationId: string;
        Status: AppointmentStatus;
    }
}