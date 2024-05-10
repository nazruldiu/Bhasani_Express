namespace BMS_Scheduler {
    export interface DailyRundownExportRequest extends Serenity.ServiceRequest {
        Id?: number;
        FileName?: string;
        ExportColumns?: string[];
    }
}
