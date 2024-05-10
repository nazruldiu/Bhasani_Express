namespace BMS_Scheduler {
    export interface TimeSegmentRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        FromTime?: number;
        ToTime?: number;
    }
}
