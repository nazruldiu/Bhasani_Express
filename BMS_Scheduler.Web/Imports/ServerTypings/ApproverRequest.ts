namespace BMS_Scheduler {
    export interface ApproverRequest extends Serenity.ServiceRequest {
        approvalProcess?: BMS_Scheduler.Common.ApprovalProcess;
    }
}
