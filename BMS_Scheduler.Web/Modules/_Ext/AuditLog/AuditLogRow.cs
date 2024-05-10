//DFA
namespace _Ext.Entities
{
    using System;
    using System.ComponentModel;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;

    [ConnectionKey("Log"), TableName("[AuditLog]")]
    [DisplayName("Audit Log"), InstanceName("Audit Log"), TwoLevelCached]
    [ReadPermission("Administration:AuditLog")]
    [ModifyPermission("Administration:AuditLog")]
    public sealed class AuditLogRow : Row<AuditLogRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, NotNull, IdProperty]
        public Int64? Id { get => Fields.Id[this]; set => Fields.Id[this] = value; }

        [DisplayName("User"), NotNull, QuickFilter]
        [LookupEditor("Administration.User")]
        public Int64? UserId { get => Fields.UserId[this]; set => Fields.UserId[this] = value; }

        [DisplayName("Action Type"), NotNull, QuickFilter]
        [AuditLogActionTypeFormatter]
        public AuditActionType? ActionType { get => Fields.ActionType[this]; set => Fields.ActionType[this] = value; }

        [DisplayName("Action Date"), NotNull, QuickFilter, SortOrder(1, descending: true)]
        [DateTimeFiltering, DateTimeFormatter]
        public DateTime? ActionDate { get => Fields.ActionDate[this]; set => Fields.ActionDate[this] = value; }

        [DisplayName("Action Date"), Expression("FORMAT(T0.ActionDate, 'dd/MM/yyyy, hh:mm:ss tt')")]
        public String FormattedActionDate { get => Fields.FormattedActionDate[this]; set => Fields.FormattedActionDate[this] = value; }

        [DisplayName("Table Name"), Size(100), Column("TableName"), NotNull, QuickFilter, QuickSearch(SearchType.Contains), NameProperty]
        public String EntityTableName { get => Fields.EntityTableName[this]; set => Fields.EntityTableName[this] = value; }

        [DisplayName("Entity Id"), NotNull, QuickFilter]
        public Int64? EntityId { get => Fields.EntityId[this]; set => Fields.EntityId[this] = value; }

        [DisplayName("Changes")]
        public String Changes { get => Fields.Changes[this]; set => Fields.Changes[this] = value; }

        [DisplayName("IP Address"), Size(100)]
        public String IpAddress { get { return Fields.IpAddress[this]; } set { Fields.IpAddress[this] = value; } }

        [DisplayName("Session Id"), Size(100)]
        public String SessionId { get => Fields.SessionId[this]; set => Fields.SessionId[this] = value; }

        [DisplayName("Feature"), Size(255), QuickSearch(SearchType.Contains)]
        public String FeatureName { get => Fields.FeatureName[this]; set => Fields.FeatureName[this] = value; }

        public class RowFields : RowFieldsBase
        {
            public Int64Field Id;
            public Int64Field UserId;
            public EnumField<AuditActionType> ActionType;
            public DateTimeField ActionDate;
            public StringField FormattedActionDate;
            public StringField EntityTableName;
            public Int64Field EntityId;
            public StringField Changes;
            public StringField IpAddress;
            public StringField SessionId;
            public StringField FeatureName;
        }

        public AuditLogRow() : base(Fields) { }
    }
}