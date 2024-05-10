namespace BMS_Scheduler.Common {
    export enum ProgramTelecastType {
        Regular = 1,
        Repeat = 2
    }
    Serenity.Decorators.registerEnumType(ProgramTelecastType, 'BMS_Scheduler.Common.ProgramTelecastType', 'ProgramTelecastType');
}
