using BMS_Scheduler.Administration.Entities;
using BMS_Scheduler.Setup;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using static Dapper.SqlMapper;

namespace BMS_Scheduler.Common
{
    public class ApprovalFlowHelper
    {
        public void GetApprovers(IDbConnection connection, ApprovalProcess approvalProcess
            , long? approvalFlowDetailId, int? approvalStatusId, long? applicationId, int userId
            , out ApprovalFlowDetailRow approvalFlowDetail
            , out List<ApprovalFlowDetailRow> allSteps
            , List<ApprovalInformationRow> approvalHistory = null)
        {
            approvalFlowDetail = new ApprovalFlowDetailRow();
            allSteps = new List<ApprovalFlowDetailRow>();

            if (approvalHistory == null)
                approvalHistory = GetApplicationHistory(connection, approvalProcess, applicationId);

            var approvers = GetApprovers(connection, approvalProcess);
            approvers.ForEach(app =>
            {
                var history = approvalHistory.OrderBy(q => q.IDate).LastOrDefault(q => q.ApprovalFlowDetailId == app.Id);
                app.ApproverId = history != null ? history.ApproverId : app.ApproverId;
                //var nextFlow = approvers.OrderBy(q => q.StepSequence).FirstOrDefault(q => q.StepSequence > app.StepSequence);
                //app.ToFlowId = nextFlow != null ? nextFlow.Id : null;
            });
            allSteps = approvers;

            if (approvalHistory == null || !approvalHistory.Any())
            {
                approvalFlowDetail = approvers.OrderBy(q => q.StepSequence).FirstOrDefault();
            }
            else
            {
                var lastHistory = approvalHistory.OrderByDescending(q => q.IDate).FirstOrDefault();
                approvalFlowDetail = approvers.LastOrDefault(q => q.Id == lastHistory.ToFlowId);
                if (approvalFlowDetail != null)
                    approvalFlowDetail.ApproverId = lastHistory.ApproverId;
            }

            if (approvalFlowDetail != null)
            {
                if (approvalStatusId == null || (Common.ApprovalStatus)approvalStatusId == Common.ApprovalStatus.Draft)
                    approvalFlowDetail.ApproverId = userId;
                if (approvalFlowDetail.ApproverId != userId)
                    approvalFlowDetail.EligibleActionList = null;

                if (approvalFlowDetail.EligibleActionList != null && approvalHistory.Any())
                {
                    var actionList = approvalFlowDetail.EligibleActionList.Where(q => q.ApprovalStatusId != (int)Common.ApprovalStatus.Delete).ToList();
                    approvalFlowDetail.EligibleActionList = actionList;
                }
            }



            //if (approvalStatusId == null && approvalFlowDetail != null)
            //{
            //    approvalFlowDetail.ApproverId = userId;
            //}
            //if (approvalStatusId != null && (Common.ApprovalStatus)approvalStatusId == Common.ApprovalStatus.Draft)
            //{
            //    approvalFlowDetail.ApproverId = userId;
            //}
            //if (approvalFlowDetail != null && approvalFlowDetail.ApproverId != userId)
            //{
            //    approvalFlowDetail.EligibleActionList = null;
            //}


            //var approvalStatusList = GetApprovalStatusList(connection);

            //var approverList = new List<ApprovalFlowDetailRow>();
            //if (approvalHistory == null || !approvalHistory.Any())
            //{
            //    approvalFlowDetail = approvers.OrderBy(q => q.StepSequence).FirstOrDefault();
            //}
            //else
            //{
            //    long? flowDetailId = null;
            //    //approvalFlowDetail = approvers.FirstOrDefault(q => q.Id == lastHistory.ToFlowId);
            //    var lastHistory = approvalHistory.OrderByDescending(q => q.IDate).FirstOrDefault();
            //    var approvalStatus = approvalStatusList.FirstOrDefault(q => q.Id == approvalStatusId);
            //    //var flowInfo = approvers.FirstOrDefault(q => q.Id == approvalFlowDetailId);

            //    switch (approvalStatus.ActionTypeId)
            //    {
            //        case ApprovalActionType.Forward:
            //            approvalFlowDetail = approvers.FirstOrDefault(q => q.Id == lastHistory.ToFlowId);
            //            if (approvalFlowDetail != null)
            //            {
            //                approvalFlowDetail.ApproverId = lastHistory.ApproverId;
            //                //approvalFlowDetail.ToFlowId = lastHistory.ToFlowId;
            //            }
            //            break;
            //        case ApprovalActionType.Backward:

            //            approvalFlowDetail = approvers.FirstOrDefault(q => q.Id == lastHistory.ToFlowId);
            //            int firstSequence = approvers.Min(q => (int)q.StepSequence);
            //            var pStep = new ApprovalFlowDetailRow();
            //            switch (approvalStatus.StepTypeId)
            //            {
            //                case ApprovalStepType.FirstStep:
            //                    pStep = approvers.FirstOrDefault(q => q.StepSequence <= firstSequence);

            //                    if (pStep != null)
            //                    {
            //                        approvalFlowDetail.ToFlowId = pStep.Id;
            //                        var appHistory = approvalHistory.OrderBy(q => q.IDate).LastOrDefault(q => q.ApprovalFlowDetailId == pStep.Id);
            //                        ////approvalFlowDetail.Id = appHistory.ApprovalFlowDetailId;
            //                        approvalFlowDetail.ApproverId = appHistory.ApplicantId;
            //                    }

            //                    break;
            //                case ApprovalStepType.PreviousStep:
            //                    int stepSequence = (int)approvalFlowDetail.StepSequence;
            //                    pStep = approvers.LastOrDefault(q => q.StepSequence < stepSequence);
            //                    if (pStep != null)
            //                    {
            //                        var appHistory = approvalHistory.OrderBy(q => q.IDate).LastOrDefault(q => q.ApprovalFlowDetailId == pStep.Id);
            //                        approvalFlowDetail.ApproverId = appHistory.ApplicantId;
            //                        approvalFlowDetail.ToFlowId = appHistory.ApprovalFlowDetailId;
            //                    }
            //                    break;
            //            }
            //            break;
            //        case ApprovalActionType.None:
            //            approvalFlowDetail = approvers.FirstOrDefault(q => q.Id == lastHistory.ToFlowId);
            //            break;
            //    }

            //}
        }

        public List<ApprovalStatusRow> GetApprovalStatusList(IDbConnection connection, int? Id = null)
        {
            var fld = ApprovalStatusRow.Fields.As("AppStatus");

            var query = new SqlQuery()
                .From(fld)
                .Select(fld.Id, fld.Name, fld.SortOrder, fld.ActionTypeId, fld.StepTypeId, fld.AccomplishedStatus)
                .Where(fld.IsActive == 1);
            if (Id != null)
                query.Where(fld.Id == (int)Id);
            return connection.Query<ApprovalStatusRow>(query).ToList();
        }

        private object GetPropValue(object source, string propertyName)
        {
            var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            return property?.GetValue(source);
        }

        private List<ApprovalFlowDetailRow> GetApprovers(IDbConnection connection, ApprovalProcess approvalProcess)
        {
            var approvalFlow = new List<ApprovalFlowDetailRow>();
            var result = connection.Query<ApproverInformationRow>(GenerateApprovalFlowQuery(approvalProcess)).ToList();
            if (result == null || !result.Any())
                throw new Exception($@"Approval flow for {EnumUtil.GetEnumDescription(approvalProcess, null)} not defined yet.");
            var flowActions = connection.Query<ApprovalFlowDetailActionsRow>(GenerateApprovalActionQuery(approvalProcess));

            var detailIds = result.OrderBy(q => q.ApprovalFlowDetailStepSequence).Select(x => x.ApprovalFlowDetailId).Distinct().ToList();
            detailIds.ForEach(d =>
            {
                var currentStep = result.FirstOrDefault(q => q.ApprovalFlowDetailId == d);
                var nextStep = result.FirstOrDefault(q => q.ApprovalFlowDetailStepSequence > currentStep.ApprovalFlowDetailStepSequence);
                var nextApprovers = new List<ApproverInformationRow>();
                var approvers = result.Where(q => q.ApprovalFlowDetailId == d && q.ApproverId != null).Select(r => new ApproverInformationRow
                {
                    ApprovalFlowDetailId = d,
                    AuthorTypeId = r.AuthorTypeId,
                    ApproverTypeId = r.ApproverTypeId,
                    ApproverId = r.ApproverId,
                    ApproverName = r.ApproverName,
                }).DistinctBy(q => q.ApproverId).ToList();

                if (nextStep != null)
                {
                    nextApprovers = result.Where(q => q.ApprovalFlowDetailId == nextStep.ApprovalFlowDetailId && q.ApproverId != null).Select(r => new ApproverInformationRow
                    {
                        ApprovalFlowDetailId = d,
                        AuthorTypeId = r.AuthorTypeId,
                        ApproverTypeId = r.ApproverTypeId,
                        ApproverId = r.ApproverId,
                        ApproverName = r.ApproverName,
                    }).DistinctBy(q => q.ApproverId).ToList();
                }

                var actions = flowActions.Where(q => q.ApprovalFlowDetailId == d).Select(r => new ApprovalFlowDetailActionsRow
                {
                    ApprovalFlowDetailId = d,
                    ApprovalStatusId = r.ApprovalStatusId,
                    ApprovalStatusName = r.ApprovalStatusName,
                    AccomplishedStatusName = r.AccomplishedStatusName,
                }).DistinctBy(q => q.ApprovalStatusId).ToList();

                var flow = result.FirstOrDefault(q => q.ApprovalFlowDetailId == d);
                var nextFlow = result.OrderBy(q => q.ApprovalFlowDetailStepSequence).FirstOrDefault(q => q.ApprovalFlowDetailStepSequence > flow.ApprovalFlowDetailStepSequence);

                var detail = new ApprovalFlowDetailRow
                {
                    Id = d,
                    EmailSubject = flow.EmailSubject,
                    EmailTemplate = flow.EmailTemplate,
                    NotificationTemplate = flow.NotificationText,
                    StepSequence = flow.ApprovalFlowDetailStepSequence,
                    ApproverDetails = approvers,
                    NextApprovers = nextApprovers,
                    EligibleActionList = actions,
                    StepTypeId = flow.ApprovalFlowDetailStepTypeId,
                    StepTypeIsInitialStep = flow.IsInitialStep,
                    StepTypeIsFinalStep = flow.IsFinalStep,
                    IsEmail = flow.IsEmail,
                    IsNotification = flow.IsNotification,
                    ToFlowId = nextFlow != null ? nextFlow.ApprovalFlowDetailId : null
                };
                approvalFlow.Add(detail);
            });

            return approvalFlow;
        }

        public List<ApprovalInformationRow> GetApplicationHistory(IDbConnection connection, ApprovalProcess approvalProcess, long? applicationId)
        {
            var appFld = ApprovalInformationRow.Fields.As("AppInfo");
            var statusFld = ApprovalStatusRow.Fields.As("AppStatus");
            var applicantFld = UserRow.Fields.As("Applicant");
            var approverFld = UserRow.Fields.As("Approver");

            var query = new SqlQuery()
                .From(appFld)
                .InnerJoin(statusFld, appFld.ApprovalStatusId == statusFld.Id)
                .InnerJoin(applicantFld, appFld.ApplicantId == applicantFld.UserId)
                .LeftJoin(approverFld, appFld.ApproverId == approverFld.UserId)

                .Where(appFld.ProcessId == (int)approvalProcess)

                .Select(appFld.Id, appFld.ApprovalStatusId, appFld.ApprovalFlowDetailId)
                .Select(statusFld.AccomplishedStatus, nameof(appFld.StatusName))
                .Select(statusFld.ActionTypeId, nameof(appFld.ActionTypeId))
                .Select(statusFld.StepTypeId, nameof(appFld.StepType))
                .Select(applicantFld.DisplayName, nameof(appFld.ApplicantName))
                .Select(approverFld.DisplayName, nameof(appFld.ApproverName))
                .Select(appFld.IDate)
                .Select(appFld.ApproverComments)
                .Select(appFld.ApproverId)
                .Select(appFld.ApplicantId)
                .Select(appFld.ToFlowId)
                .OrderBy(appFld.IDate, desc: true)
                ;

            if (applicationId != null)
            {
                query.Where(appFld.ApplicationId == applicationId.Value);
            }
            else
            {
                query.Where(appFld.ApplicationId == -1);
            }
            return connection.Query<ApprovalInformationRow>(query).ToList();

        }

        private SqlQuery GenerateApprovalFlowQuery(ApprovalProcess approvalProcess)
        {
            var flowFld = ApprovalFlowRow.Fields.As("Flow");
            var flowDetailFld = ApprovalFlowDetailRow.Fields.As("FlowDetail");
            var approverFld = ApproverInformationRow.Fields.As("Approver");
            var designationFld = DesignationRow.Fields.As("Designation");
            var designationUserFld = UserRow.Fields.As("DsgUser");
            var userFld = UserRow.Fields.As("aUser");
            var stetpTypeFld = ApprovalStepTypeRow.Fields.As("StepType");
            var query = new SqlQuery()
                .From(flowFld)
                .LeftJoin(flowDetailFld, flowFld.Id == flowDetailFld.ApprovalFlowId)
                .LeftJoin(approverFld, flowDetailFld.Id == approverFld.ApprovalFlowDetailId)
                .LeftJoin(userFld, approverFld.ApproverId == userFld.UserId)
                .LeftJoin(designationFld, approverFld.DesignationId == designationFld.Id)
                .LeftJoin(designationUserFld, designationFld.Id == designationUserFld.DesignationId)
                //.LeftJoin(appActionFld, flowDetailFld.Id == appActionFld.ApprovalFlowDetailId)
                //.LeftJoin(appStatusFld, appActionFld.ApprovalStatusId == appStatusFld.Id)
                .LeftJoin(stetpTypeFld, flowDetailFld.StepTypeId == stetpTypeFld.Id)
                .Where(flowFld.ApprovalProcessId == (int)approvalProcess)

                .Select(flowDetailFld.Id, nameof(ApproverInformationRow.ApprovalFlowDetailId))
                .Select(flowDetailFld.StepSequence, nameof(ApproverInformationRow.ApprovalFlowDetailStepSequence))
                .Select(approverFld.AuthorTypeId)
                .Select(approverFld.ApproverTypeId)
                .Select("Coalesce(Approver.ApproverId,DsgUser.UserId)", nameof(approverFld.ApproverId))
                .Select("Coalesce(aUser.DisplayName,DsgUser.DisplayName)", nameof(approverFld.ApproverName))
                .Select(flowDetailFld.EmailSubject, nameof(ApproverInformationRow.EmailSubject))
                .Select(flowDetailFld.EmailTemplate, nameof(ApproverInformationRow.EmailTemplate))
                .Select(flowDetailFld.NotificationTemplate, nameof(ApproverInformationRow.NotificationText))
                .Select(flowDetailFld.StepTypeId, nameof(ApproverInformationRow.ApprovalFlowDetailStepTypeId))
                .Select(stetpTypeFld.IsInitialStep, nameof(ApproverInformationRow.IsInitialStep))
                .Select(stetpTypeFld.IsFinalStep, nameof(ApproverInformationRow.IsFinalStep))
                .Select(flowDetailFld.IsEmail, nameof(ApproverInformationRow.IsEmail))
                .Select(flowDetailFld.IsNotification, nameof(ApproverInformationRow.IsNotification))
                .OrderBy(flowDetailFld.StepSequence)
                .OrderBy(stetpTypeFld.IsInitialStep)
                //.OrderBy(appStatusFld.SortOrder)
                ;

            return query;


        }

        private SqlQuery GenerateApprovalActionQuery(ApprovalProcess approvalProcess)
        {
            var flowFld = ApprovalFlowRow.Fields.As("Flow");
            var flowDetailFld = ApprovalFlowDetailRow.Fields.As("FlowDetail");
            var appActionFld = ApprovalFlowDetailActionsRow.Fields.As("FlowAction");
            var appStatusFld = ApprovalStatusRow.Fields.As("AppStatus");

            var query = new SqlQuery()
                .From(flowFld)
                .InnerJoin(flowDetailFld, flowFld.Id == flowDetailFld.ApprovalFlowId)
                .LeftJoin(appActionFld, flowDetailFld.Id == appActionFld.ApprovalFlowDetailId)
                .LeftJoin(appStatusFld, appActionFld.ApprovalStatusId == appStatusFld.Id)
                .Select(appActionFld.ApprovalFlowDetailId)
                .Select(appActionFld.ApprovalStatusId)
                .Select(appActionFld.AccomplishedStatusName)
                .Select(appStatusFld.Name, nameof(ApprovalFlowDetailActionsRow.ApprovalStatusName))
                .Where(flowFld.ApprovalProcessId == (int)approvalProcess)
                .OrderBy(flowDetailFld.Id);

            return query;
        }

        public bool IsApprovalRequired(IDbConnection connection, ApprovalProcess process)
        {
            var fld = ApprovalFlowScopeRow.Fields;
            var now = DateTime.Now.Date;

            var query = new SqlQuery()
                .From(fld)
                .Where(fld.ApprovalProcess == (int)process)
                .Where(fld.IsContinue == 1 || (fld.EffectiveFrom <= now && fld.EffectiveTo >= now))
                .Select(fld.Id);
            var result = connection.Query<ApprovalFlowScopeRow>(query);

            return result != null && result.Any() ? true : false;

        }

    }
}
