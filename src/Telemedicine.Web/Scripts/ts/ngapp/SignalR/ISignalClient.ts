module Telemedicine {
    export interface ISignalClient {
        onDoctorUpdated(doctor: IDoctor);
    }
}