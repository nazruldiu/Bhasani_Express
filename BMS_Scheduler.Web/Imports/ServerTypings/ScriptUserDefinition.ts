namespace BMS_Scheduler {
    export interface ScriptUserDefinition {
        Username?: string;
        DisplayName?: string;
        IsAdmin?: boolean;
        Permissions?: { [key: string]: boolean };
        TVChannelId?: number;
        UserId?: number;
    }
}
