
namespace _Ext.Forms
{
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [FormScript("_Ext.AuditLog")]
    [BasedOnRow(typeof(Entities.AuditLogRow))]
    public class AuditLogForm
    {
        [HalfWidth(UntilNext = true)]
        [ReadOnly(true)]
        public String FeatureName { get; set; }
        [ReadOnly(true)]
        public String EntityTableName { get; set; }
        [ReadOnly(true)]
        public AuditActionType ActionType { get; set; }
        [ReadOnly(true)]
        public String FormattedActionDate { get; set; }

        [StaticTextBlock(IsHtml = true), ResetFormWidth(JustThis = true)]
        public String Changes { get; set; }
        [ReadOnly(true)]
        public Int64 UserId { get; set; }
        [ReadOnly(true)]
        public String IpAddress { get; set; }
        [ReadOnly(true)]
        public String SessionId { get; set; }
        [ReadOnly(true)]
        public Int32 EntityId { get; set; }
    }
}