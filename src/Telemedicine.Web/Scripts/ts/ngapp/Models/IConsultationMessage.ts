module Telemedicine {
    export interface IConsultationMessage {
        Direction: string;
        AvatarUrl: string; 
        Created: Date;
        Message: string;
    }
}