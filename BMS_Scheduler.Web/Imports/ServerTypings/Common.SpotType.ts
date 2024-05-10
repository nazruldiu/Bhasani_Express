namespace BMS_Scheduler.Common {
    export enum SpotType {
        Paid = 1,
        Bonus = 2
    }
    Serenity.Decorators.registerEnumType(SpotType, 'BMS_Scheduler.Common.SpotType', 'SpotType');
}
