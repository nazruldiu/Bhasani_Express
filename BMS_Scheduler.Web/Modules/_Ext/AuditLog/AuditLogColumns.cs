
namespace _Ext.Columns
{
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [ColumnsScript("_Ext.AuditLog")]
    [BasedOnRow(typeof(Entities.AuditLogRow))]
    public class AuditLogColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int64 Id { get; set; }
        [QuickFilter]
        public String FeatureName { get; set; }
        
        public AuditActionType ActionType { get; set; }
        [DisplayFormat("dd/MM/yyyy hh:mm:ss tt"), Width(170, Min = 170)]
        public String ActionDate { get; set; }
        //public Int64 EntityId { get; set; }
        [Hidden]
        public String Changes { get; set; }
        [Width(200, Min = 150)]
        public Int64 UserId { get; set; }
        public String IpAddress { get; set; }
        public String EntityTableName { get; set; }
    }
}