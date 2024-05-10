namespace BMS_Scheduler.Common {
    export enum RundownPositionPermission {
        Commercial = 10,
        SegmentBody = 20,
        All = 100
    }
    Serenity.Decorators.registerEnumType(RundownPositionPermission, 'BMS_Scheduler.Common.RundownPositionPermission', 'RundownPositionPermission');
}
