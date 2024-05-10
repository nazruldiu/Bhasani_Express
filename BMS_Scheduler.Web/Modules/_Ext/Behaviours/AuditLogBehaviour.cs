using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using _Ext.Entities;
using Microsoft.AspNetCore.Http;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Services;
using System.Linq;


namespace _Ext
{
    public class AuditRowBehavior : BaseSaveDeleteBehavior, IImplicitBehavior
    {
        protected ISqlConnections SqlConnections { get; }
        public HttpContext HttpContext { get; }

        public AuditRowBehavior(ISqlConnections sqlConnections, IHttpContextAccessor httpContextAccessor)
        {
            SqlConnections = sqlConnections;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public bool ActivateFor(IRow row)
        {
            return row is IAuditLog;
        }

        public override void OnAudit(ISaveRequestHandler handler)
        {
            if (handler.IsCreate)
            {
                InsertNewLog(handler.Context, handler.Row, handler.Old, AuditActionType.Insert);
            }

            if (handler.IsUpdate)
            {
                InsertNewLog(handler.Context, handler.Row, handler.Old, AuditActionType.Update);
            }
        }

        public override void OnAudit(IDeleteRequestHandler handler)
        {
            InsertNewLog(handler.Context, handler.Row, null, AuditActionType.Delete);
        }

        private void InsertNewLog(IRequestContext context, IRow row, IRow oldRow, AuditActionType auditActionType)
        {
            try
            {
                using var auditLogConnection = SqlConnections.NewFor<AuditLogRow>();
                var fld = AuditLogRow.Fields;

                var entityId = row.IdField.AsObject(row);
                GetFeatureName(row, out string featureName);

                var changes = GetChanges(row, oldRow);

                if (changes.Count > 0)
                {
                    var auditLogRow = new AuditLogRow
                    {
                        UserId = Convert.ToInt64(context.User.GetIdentifier()),
                        ActionType = auditActionType,
                        ActionDate = DateTime.Now,
                        EntityTableName = row.Table,
                        EntityId = Convert.ToInt64(entityId.ToString()),
                        Changes = changes.ToJson(),
                        IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                        SessionId = HttpContext.TraceIdentifier,
                        FeatureName = featureName
                    };

                    auditLogConnection.Insert(auditLogRow);
                }
            }
            catch (Exception ex)
            {
                ex.Log(null);
            }
        }

        private void GetFeatureName(IRow row, out string featureName)
        {
            var attribute = row.GetType().GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true).Cast<System.ComponentModel.DisplayNameAttribute>().Single();
            if (attribute != null)
                featureName = attribute.DisplayName;
            else
                featureName = row.Table;

            if ((Regex.Matches(featureName, @"((\w+(\s?)))").Count) == 1)
                featureName = string.Join(" ", SplitCamelCase(featureName.Trim()));
        }

        private IEnumerable<string> SplitCamelCase(string input)
        {
            return Regex.Split(input, @"([A-Z]?[a-z]+)").Where(str => !string.IsNullOrEmpty(str));
        }

        private static Dictionary<string, object[]> GetChanges(IRow row, IRow oldRow)
        {
            var changes = new Dictionary<string, object[]>();
            var tableFields = row.EnumerateTableFields();

            foreach (var field in tableFields)
            {
                if (row.IsAssigned(field))
                {
                    var oldValue = oldRow?[field.Name];
                    var newValue = row[field.Name];
                    if (!Equals(oldValue, newValue))
                    {
                        var fieldChange = new object[] { oldValue, newValue };
                        changes.Add(field.Name, fieldChange);
                    }
                }
            }

            return changes;
        }
    }

    /// <summary>
    /// This interface is used to log the changes for Insert / Update and Delete.
    /// </summary>
    public interface IAuditLog
    {
    }


    [EnumKey("Enum.Audit.AuditActionType"), ScriptInclude]
    public enum AuditActionType
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }

    /// <summary>
    /// Any field which does not required to log in audit table. For Example InsertUserId, InsertDate etc
    /// </summary>
    //public class IgnoreAuditLogAttribute : Attribute
    //{
    //}
}