using BMS_Scheduler.Common;
using BMS_Scheduler.Setup;
using Microsoft.AspNetCore.Http;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using _Apv;

namespace BMS_Scheduler
{
    public class ApprovalFlowBehaviour : BaseSaveBehavior, IImplicitBehavior
    {
        protected ISqlConnections SqlConnections { get; }
        public HttpContext HttpContext { get; }
        public ApprovalFlowBehaviour(ISqlConnections sqlConnections, IHttpContextAccessor httpContextAccessor)
        {
            SqlConnections = sqlConnections;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public bool ActivateFor(IRow row)
        {
            return row is IApprovalFlow;
        }

        public override void OnBeforeSave(ISaveRequestHandler handler)
        {
            base.OnBeforeSave(handler);


            Type type = handler.Row.GetType();
            var approvalFlowProp = type.GetCustomAttributes(typeof(ApprovalFlow), true).FirstOrDefault() as _Apv.ApprovalFlow;
            if (approvalFlowProp != null)
            {
                ApprovalProcess approvalProcess = (ApprovalProcess)approvalFlowProp.ApprovalProcess;
                bool isApprovalRequired = new ApprovalFlowHelper().IsApprovalRequired(connection: handler.Connection, approvalProcess);
                if (isApprovalRequired == false)
                    return;
                var approvalStatusProp = type.GetProperties().FirstOrDefault(prop =>
                        Attribute.IsDefined(prop, typeof(_Apv.ApprovalStatus)));
                var approvalStatusValue = approvalStatusProp.GetValue(handler.Row, null);

                var approverProp = type.GetProperties().FirstOrDefault(prop =>
                        Attribute.IsDefined(prop, typeof(Approver)));
                var approverValue = approverProp.GetValue(handler.Row, null);

                if (approvalStatusValue != null)
                {
                    switch ((Common.ApprovalStatus)approvalStatusValue)
                    {
                        case Common.ApprovalStatus.Recommend:
                        case Common.ApprovalStatus.Submit:
                        case Common.ApprovalStatus.ReSubmit:
                            if (approverValue == null)
                            {
                                throw new Exception("Please Select Recommender.");
                            }
                            break;
                    }
                }
            }
        }

        public override void OnAfterSave(ISaveRequestHandler handler)
        {
            base.OnAfterSave(handler);



            Type type = handler.Row.GetType();
            var approvalStatus = GetApprovalStatus(handler, type);

            if (approvalStatus == null
                || (Common.ApprovalStatus)approvalStatus == Common.ApprovalStatus.Draft)
            {
                return;
            }

            var approvalFlowProp = type.GetCustomAttributes(typeof(ApprovalFlow), true).FirstOrDefault() as _Apv.ApprovalFlow;
            if (approvalFlowProp != null)
            {
                int userId = Convert.ToInt32(handler.Context.User.GetIdentifier());
                ApprovalProcess approvalProcess = (ApprovalProcess)approvalFlowProp.ApprovalProcess;
                bool isApprovalRequired = new ApprovalFlowHelper().IsApprovalRequired(connection: handler.Connection, approvalProcess);
                if (isApprovalRequired == false)
                    return;

                var entityId = handler.Row.IdField.AsObject(handler.Row);
                int? approverId = GetApproverId(handler, type);
                string approverComment = GetApproverComment(handler, type);

                var history = GetApprovalHistory(handler, type);

                long? lastExecutedFlowId = history != null && history.Any()
                    ? history.OrderBy(q => q.IDate).LastOrDefault().ApprovalFlowDetailId
                    : null;

                var approvalStatusList = new ApprovalFlowHelper().GetApprovalStatusList(handler.Connection, approvalStatus != null ? (int)approvalStatus : null);
                var status = approvalStatusList.FirstOrDefault(q => q.Id == (int)approvalStatus);
                new ApprovalFlowHelper().GetApprovers(connection: handler.Connection, approvalProcess: approvalProcess, approvalFlowDetailId: lastExecutedFlowId, approvalStatusId: approvalStatus != null ? (int)approvalStatus : null, (long)entityId, userId: userId, approvalFlowDetail: out ApprovalFlowDetailRow flowDetail, allSteps: out List<ApprovalFlowDetailRow> allSteps, approvalHistory: history);


                switch (status.ActionTypeId)
                {
                    case Common.ApprovalActionType.Backward:
                        switch (status.StepTypeId)
                        {
                            case Common.ApprovalStepType.FirstStep:
                                flowDetail.ApproverId = history.OrderBy(q => q.IDate).FirstOrDefault().ApplicantId;
                                flowDetail.ToFlowId = history.OrderBy(q => q.IDate).FirstOrDefault().ApprovalFlowDetailId;
                                break;
                            case Common.ApprovalStepType.PreviousStep:
                                var previousStep = allSteps.LastOrDefault(q => q.StepSequence < flowDetail.StepSequence);
                                var lastHistory = history.FirstOrDefault(q => q.ApprovalFlowDetailId == previousStep.Id);
                                flowDetail.ToFlowId = lastHistory.ApprovalFlowDetailId;
                                flowDetail.ApproverId = lastHistory.ApplicantId;
                                break;
                        }
                        break;

                }

                //new ApprovalFlowHelper().GetApprovers(handler.Connection, approvalProcess, lastExecutedFlowId, approvalStatus != null ? (int)approvalStatus : null, (long)entityId, out ApprovalFlowDetailRow flowDetail, approvalHistory: history);
                //long? toFlowId = flowDetail.ToFlowId;
                //if (approvalStatus != null)
                //{
                //    switch (status.ActionTypeId)
                //    {
                //        case Common.ApprovalActionType.Backward:
                //            approverId = flowDetail.ApproverId;
                //            //toFlowId = flowDetail.Id;
                //            break;
                //    }
                //    //if ((Common.ApprovalStatus)approvalStatus == Common.ApprovalStatus.Reject)
                //    //{
                //    //    approverId = flowDetail.ApproverId;
                //    //}
                //}

                var approvalEntity = new ApprovalInformationRow
                {
                    ProcessId = approvalProcess,
                    ApplicationId = (long)entityId,
                    ApprovalFlowDetailId = flowDetail.Id,//FlowDetailId
                    ApplicantId = Convert.ToInt32(handler.Context.User.GetIdentifier()),
                    ApproverId = approverId == null ? flowDetail.ApproverId : approverId,//Approver
                    ApprovalStatusId = (int)approvalStatus, //AppStatus
                    ApplicationUrl = null,
                    ApproverComments = approverComment != null ? approverComment.ToString() : null,
                    IUser = handler.Context.User.GetIdentifier(),
                    IDate = DateTime.Now,
                    ToFlowId = flowDetail.ToFlowId
                };
                handler.Connection.Insert<ApprovalInformationRow>(approvalEntity);
                new EmailHelper().SendEmailAndNotification(handler, flowDetail.EmailTemplate, flowDetail.NotificationTemplate);

            }

            //new ApprovalFlowHelper().GetApprover(handler.Connection,)

        }

        private Common.ApprovalStatus? GetApprovalStatus(ISaveRequestHandler handler, Type type)
        {
            var approvalStatusProp = type.GetProperties().FirstOrDefault(prop =>
                                    Attribute.IsDefined(prop, typeof(_Apv.ApprovalStatus)));

            var approvalStatus = approvalStatusProp != null
                ? approvalStatusProp.GetValue(handler.Row, null)
                : null;
            return approvalStatus != null ? (Common.ApprovalStatus)approvalStatus : null;
        }

        private string GetApproverComment(ISaveRequestHandler handler, Type type)
        {
            var approverCommentProp = type.GetProperties().FirstOrDefault(prop =>
                                    Attribute.IsDefined(prop, typeof(ApproverComment)));
            var approverComment = approverCommentProp.GetValue(handler.Row, null);
            return approverComment != null ? approverComment.ToString() : null;
        }

        private List<ApprovalInformationRow> GetApprovalHistory(ISaveRequestHandler handler, Type type)
        {
            var approvalHistroyProp = type.GetProperties().FirstOrDefault(prop =>
                                    Attribute.IsDefined(prop, typeof(ApprovalHistory)));
            var history = approvalHistroyProp.GetValue(handler.Row, null);
            return history != null ? (List<ApprovalInformationRow>)history : null;
        }

        private int? GetApproverId(ISaveRequestHandler handler, Type type)
        {
            var approverProp = type.GetProperties().FirstOrDefault(prop =>
                                    Attribute.IsDefined(prop, typeof(Approver)));
            var approverValue = approverProp.GetValue(handler.Row, null);
            int? approverId = approverValue != null ? (int)approverValue : null;
            return approverId;
        }
    }




    public class ApprovalFlowHistoryBehaviour : BaseRetrieveBehavior, IImplicitBehavior
    {
        protected ISqlConnections SqlConnections { get; }
        public HttpContext HttpContext { get; }
        public ApprovalFlowHistoryBehaviour(ISqlConnections sqlConnections, IHttpContextAccessor httpContextAccessor)
        {
            SqlConnections = sqlConnections;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public bool ActivateFor(IRow row)
        {
            return row is IApprovalFlow;
        }

        public override void OnReturn(IRetrieveRequestHandler handler)
        {
            base.OnReturn(handler);

            Type type = handler.Row.GetType();

            int userId = Convert.ToInt32(handler.Context.User.GetIdentifier());

            var approvalFlowProp = type.GetCustomAttributes(typeof(ApprovalFlow), true).FirstOrDefault() as ApprovalFlow;
            if (approvalFlowProp != null)
            {
                var approvalProcess = (ApprovalProcess)approvalFlowProp.ApprovalProcess;

                bool isApprovalRequired = new ApprovalFlowHelper().IsApprovalRequired(connection: handler.Connection, approvalProcess);
                if (isApprovalRequired == false)
                    return;

                var approvalHistoryProp = type.GetProperties().FirstOrDefault(prop =>
                Attribute.IsDefined(prop, typeof(ApprovalHistory)));

                if (approvalHistoryProp != null)
                {
                    var appHistoryAttribute = type.GetProperty(approvalHistoryProp.Name)
                                     .GetCustomAttribute<ApprovalHistory>();

                    var idProperty = type.GetProperties().FirstOrDefault(prop =>
                        Attribute.IsDefined(prop, typeof(Serenity.Data.IdPropertyAttribute)));

                    var approvalStatusProp = type.GetProperties().FirstOrDefault(prop =>
                        Attribute.IsDefined(prop, typeof(_Apv.ApprovalStatus)));

                    var flowDetailProp = type.GetProperties().FirstOrDefault(prop =>
                       Attribute.IsDefined(prop, typeof(ApprovalFlowDetail)));

                    var idValue = idProperty.GetValue(handler.Row, null);
                    if (idValue != null)
                    {
                        var history = new ApprovalFlowHelper().GetApplicationHistory(connection: handler.Connection, approvalProcess, (long)idValue);
                        approvalHistoryProp.SetValue(handler.Row, history, null);

                        long? currentFlowId = null;
                        if (history != null && history.Any())
                        {
                            currentFlowId = history.OrderBy(q => q.IDate).LastOrDefault().ApprovalFlowDetailId;
                        }
                        //GetApprovalHistory(handler.Connection, handler.Row, approvalProcess, idValue, approvalHistoryProp);

                        var approvalStatusValue = approvalStatusProp.GetValue(handler.Row, null);


                        //if (approvalStatusValue != null)
                        //{
                        new ApprovalFlowHelper().GetApprovers(handler.Connection, approvalProcess, currentFlowId, approvalStatusValue != null ? (int)approvalStatusValue : null, (long)idValue, userId: userId, out ApprovalFlowDetailRow flowDetail, out List<ApprovalFlowDetailRow> allSteps);

                        if (flowDetailProp != null)
                            flowDetailProp.SetValue(handler.Row, flowDetail, null);
                        //}

                    }

                }
            }
        }

        private void GetApprovalHistory(IDbConnection connection, IRow row, ApprovalProcess approvalProcess, object idValue, PropertyInfo prop)
        {
            var fld = ApprovalInformationRow.Fields.As("Approval");
            long id = Convert.ToInt64(idValue);
            var query = new SqlQuery()
                .From(fld)
                .Where(fld.ApplicationId == id
                    && fld.ProcessId == (int)approvalProcess)
                .Select(fld.Id);
            var history = connection.Query<ApprovalInformationRow>(query);
            prop.SetValue(row, history, null);

        }
    }



    public interface IApprovalFlow
    {
        //public ApprovalFlowDetailRow ApprovalFlowInformation { get; set; }
    }
}

namespace _Apv
{
    using BMS_Scheduler.Common;

    #region Attributes

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApprovalFlow : Attribute
    {
        ApprovalProcess approvalProcess;
        public ApprovalFlow(ApprovalProcess processName)
        {
            this.approvalProcess = processName;
        }

        public ApprovalProcess ApprovalProcess
        {
            get { return approvalProcess; }
        }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ApprovalStatus : Attribute
    {
        public ApprovalStatus()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Approver : Attribute
    {
        public Approver()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ApproverComment : Attribute
    {
        public ApproverComment()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ApprovalHistory : Attribute
    {
        public ApprovalHistory()
        {

        }
        //Common.ApprovalProcess approvalProcess;
        //public ApprovalHistory(Common.ApprovalProcess processName)
        //{
        //    this.approvalProcess = processName;
        //}

        //public Common.ApprovalProcess ApprovalProcess
        //{
        //    get { return approvalProcess; }
        //}
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ApprovalFlowDetail : Attribute
    {
        public ApprovalFlowDetail()
        {

        }
        //Common.ApprovalProcess approvalProcess;
        //public ApprovalHistory(Common.ApprovalProcess processName)
        //{
        //    this.approvalProcess = processName;
        //}

        //public Common.ApprovalProcess ApprovalProcess
        //{
        //    get { return approvalProcess; }
        //}
    }

    #endregion
}
