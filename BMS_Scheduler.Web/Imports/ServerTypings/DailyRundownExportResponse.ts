namespace BMS_Scheduler {
    export interface DailyRundownExportResponse extends Serenity.ServiceResponse {
        Rundowns?: DailyRundownInformation[];
    }
}
