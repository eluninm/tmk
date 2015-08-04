module Telemedicine {
    export interface ITimelineDate {
        Date: Date;
        HasConsultations: Boolean;
        Hours: Array<ITimelineHour>;
    }

    export interface ITimelineHour {
        Hour: number;
        HourType: TimelineHourType;
        PatientsCount: number;
    }

    export enum TimelineHourType {
        Working,
        NotWorking,
        Clear
    } 
}