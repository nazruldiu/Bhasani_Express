namespace BMS_Scheduler.Common {
    export enum UserType {
        Internal = 1,
        External = 2
    }
    Serenity.Decorators.registerEnumType(UserType, 'BMS_Scheduler.Common.UserType', 'UserType');
}
