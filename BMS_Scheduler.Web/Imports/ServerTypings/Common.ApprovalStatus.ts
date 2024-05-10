namespace BMS_Scheduler.Common {
    export enum ApprovalStatus {
        Draft = 1,
        Submit = 2,
        Recommend = 3,
        Approve = 4,
        Reject = 5,
        Cancel = 6,
        ReSubmit = 7,
        Delete = 8
    }
    Serenity.Decorators.registerEnumType(ApprovalStatus, 'BMS_Scheduler.Common.ApprovalStatus', 'ApprovalStatus');
}
