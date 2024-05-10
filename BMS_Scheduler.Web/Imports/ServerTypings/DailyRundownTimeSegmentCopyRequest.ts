namespace BMS_Scheduler {
    export interface DailyRundownTimeSegmentCopyRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        RundownTimeSegmentId?: number;
    }
}
