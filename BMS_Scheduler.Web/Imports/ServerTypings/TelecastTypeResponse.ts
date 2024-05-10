namespace BMS_Scheduler {
    export interface TelecastTypeResponse extends Serenity.ServiceResponse {
        TelecastType?: BMS_Scheduler.Common.ProgramTelecastType;
        EpisodeNumber?: number;
    }
}
