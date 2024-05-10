namespace BMS_Scheduler {
    export interface ApprovalFlowRequest extends Serenity.ServiceRequest {
        approvalProcess?: BMS_Scheduler.Common.ApprovalProcess;
        ApprovalFlowDetailId?: number;
        ApprovalStatusId?: number;
        ApplicationId?: number;
    }
}
