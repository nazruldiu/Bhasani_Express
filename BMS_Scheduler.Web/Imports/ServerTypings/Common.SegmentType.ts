namespace BMS_Scheduler.Common {
    export enum SegmentType {
        ByTimeSlot = 1,
        ByProgramBanner = 2,
        ByProgramCategory = 3,
        ByProgramName = 4
    }
    Serenity.Decorators.registerEnumType(SegmentType, 'BMS_Scheduler.Common.SegmentType', 'SegmentType');
}
