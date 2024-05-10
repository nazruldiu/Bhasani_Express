namespace BMS_Scheduler {
    export interface TimeSegmentResponse extends Serenity.ServiceResponse {
        StartTime?: string;
        EndTime?: string;
        IsSuccess?: boolean;
        Message?: string;
    }
}
