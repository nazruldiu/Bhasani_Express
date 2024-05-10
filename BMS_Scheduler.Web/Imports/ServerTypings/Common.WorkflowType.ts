namespace BMS_Scheduler.Common {
    export enum WorkflowType {
        FPC = 1,
        RO = 2,
        Rundown = 3,
        TimeSlotSegment = 4
    }
    Serenity.Decorators.registerEnumType(WorkflowType, 'BMS_Scheduler.Common.WorkflowType', 'WorkflowType');
}
