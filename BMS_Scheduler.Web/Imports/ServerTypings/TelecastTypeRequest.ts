namespace BMS_Scheduler {
    export interface TelecastTypeRequest extends Serenity.ServiceRequest {
        ProgramId?: number;
        EpisodeNumber?: number;
        SegmentNumber?: number;
        TelecastTime?: string;
    }
}
