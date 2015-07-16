module Telemedicine {
    export interface Recommendation {
        RecommendationId: number;
        Created: Date;
        DoctorTitle: string;
        DoctorSpecialization: string;
        RecommendationText: string;
    }
}