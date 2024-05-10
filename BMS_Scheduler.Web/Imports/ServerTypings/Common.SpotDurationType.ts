namespace BMS_Scheduler.Common {
    export enum SpotDurationType {
        BySpot = 1,
        BySecond = 2
    }
    Serenity.Decorators.registerEnumType(SpotDurationType, 'BMS_Scheduler.Common.SpotDurationType', 'SpotDurationType');
}
