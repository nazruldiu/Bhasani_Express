namespace BMS_Scheduler.Common {
    export enum ApprovalProcess {
        FPC = 1,
        RO = 2,
        ClientContract = 3,
        Invoice = 4
    }
    Serenity.Decorators.registerEnumType(ApprovalProcess, 'BMS_Scheduler.Common.ApprovalProcess', 'ApprovalProcess');
}
