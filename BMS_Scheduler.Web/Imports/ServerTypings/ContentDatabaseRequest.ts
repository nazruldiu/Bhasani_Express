namespace BMS_Scheduler {
    export interface ContentDatabaseRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        StartTime?: string;
        EndTime?: string;
        ProgramId?: number;
        ProgramCategoryId?: number;
        SpotPositionId?: number;
    }
}
