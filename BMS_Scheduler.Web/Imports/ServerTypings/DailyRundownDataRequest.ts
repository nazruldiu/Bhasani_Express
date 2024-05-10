namespace BMS_Scheduler {
    export interface DailyRundownDataRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        TimeSegmentId?: number;
        PositionId?: number;
    }
}
