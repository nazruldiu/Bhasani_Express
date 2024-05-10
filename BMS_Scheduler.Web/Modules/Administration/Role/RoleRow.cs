using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace BMS_Scheduler.Administration.Entities
{
    [ConnectionKey("Default"), Module("Administration"), TableName("Roles")]
    [DisplayName("Roles"), InstanceName("Role")]
    [NavigationPermission(NavigationPermissionKey)]
    [ReadPermission(ReadPermissionKey)]
    [InsertPermission(InsertPermissionKey)]
    [UpdatePermission(UpdatePermissionKey)]
    [DeletePermission(DeletePermissionKey)]
    [LookupScript]
    public sealed class RoleRow : Row<RoleRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Role Id"), Identity, ForeignKey("Roles", "RoleId"), LeftJoin("jRole"), IdProperty]
        public Int32? RoleId
        {
            get => fields.RoleId[this];
            set => fields.RoleId[this] = value;
        }

        [DisplayName("Role Name"), Size(100), NotNull, QuickSearch, NameProperty]
        public String RoleName
        {
            get => fields.RoleName[this];
            set => fields.RoleName[this] = value;
        }


        public RoleRow()
        {
        }

        public RoleRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field RoleId;
            public StringField RoleName;
        }

        [Description("Navigation"), ImplicitPermission(ReadPermissionKey)]
        public const string NavigationPermissionKey = "Administration:Role:Navigation";
        [Description("View")]
        public const string ReadPermissionKey = "Administration:Role:Read";
        [Description("Insert")]
        public const string InsertPermissionKey = "Administration:Role:Insert";
        [Description("Update")]
        public const string UpdatePermissionKey = "Administration:Role:Update";
        [Description("Delete")]
        public const string DeletePermissionKey = "Administration:Role:Delete";
    }
}