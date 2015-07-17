module Telemedicine {
    export interface IRecommendation {
        Id: number;
        Created: Date;
        DoctorTitle: string;
        DoctorSpecialization: string;
        RecommendationText: string;
    }
}