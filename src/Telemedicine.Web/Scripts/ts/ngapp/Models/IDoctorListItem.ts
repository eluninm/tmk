module Telemedicine {
    export interface IDoctor {
        Id: number;
        Title: string;
        Specialization: string;
        AvatarUrl: string;
        StatusText: string;
        StatusName: string;
        IsChatAvailable: boolean;
        IsAudioAvailable: boolean;
        IsVideoAvailable: boolean;
    }
}