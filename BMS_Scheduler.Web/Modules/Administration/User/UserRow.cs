using BMS_Scheduler.Common;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BMS_Scheduler.Administration.Entities
{
    [ConnectionKey("Default"), Module("Administration"), TableName("Users")]
    [DisplayName("Users"), InstanceName("User")]
    [NavigationPermission(PermissionKeys.Security)]
    [ReadPermission(ReadPermissionKey)]
    [InsertPermission(InsertPermissionKey)]
    [UpdatePermission(UpdatePermissionKey)]
    [DeletePermission(DeletePermissionKey)]
    [LookupScript(Permission = "?")]
    public sealed class UserRow : LoggingRow<UserRow.RowFields>, IIdRow, INameRow, IIsActiveRow
    {
        [DisplayName("User Id"), Identity, IdProperty]
        public Int32? UserId
        {
            get => fields.UserId[this];
            set => fields.UserId[this] = value;
        }

        [DisplayName("Username"), Size(100), NotNull, QuickSearch, LookupInclude]
        public String Username
        {
            get => fields.Username[this];
            set => fields.Username[this] = value;
        }

        [DisplayName("Source"), Size(4), NotNull, Insertable(false), Updatable(false), DefaultValue("site")]
        public String Source
        {
            get => fields.Source[this];
            set => fields.Source[this] = value;
        }

        [DisplayName("Password Hash"), Size(86), NotNull, Insertable(false), Updatable(false), MinSelectLevel(SelectLevel.Never)]
        public String PasswordHash
        {
            get => fields.PasswordHash[this];
            set => fields.PasswordHash[this] = value;
        }

        [DisplayName("Password Salt"), Size(10), NotNull, Insertable(false), Updatable(false), MinSelectLevel(SelectLevel.Never)]
        public String PasswordSalt
        {
            get => fields.PasswordSalt[this];
            set => fields.PasswordSalt[this] = value;
        }

        [DisplayName("Display Name"), Size(100), NotNull, LookupInclude, NameProperty]
        public String DisplayName
        {
            get => fields.DisplayName[this];
            set => fields.DisplayName[this] = value;
        }

        [DisplayName("Email"), Size(100)]
        public String Email
        {
            get => fields.Email[this];
            set => fields.Email[this] = value;
        }

        [DisplayName("User Image"), Size(100)]
        [ImageUploadEditor(FilenameFormat = "UserImage/~", CopyToHistory = true)]
        public String UserImage
        {
            get => fields.UserImage[this];
            set => fields.UserImage[this] = value;
        }

        [DisplayName("Password"), Size(50), NotMapped]
        public String Password
        {
            get => fields.Password[this];
            set => fields.Password[this] = value;
        }

        [NotNull, Insertable(false), Updatable(true)]
        public Int16? IsActive
        {
            get => fields.IsActive[this];
            set => fields.IsActive[this] = value;
        }

        [DisplayName("Confirm Password"), Size(50), NotMapped]
        public String PasswordConfirm
        {
            get => fields.PasswordConfirm[this];
            set => fields.PasswordConfirm[this] = value;
        }

        [DisplayName("Last Directory Update"), Insertable(false), Updatable(false)]
        public DateTime? LastDirectoryUpdate
        {
            get => fields.LastDirectoryUpdate[this];
            set => fields.LastDirectoryUpdate[this] = value;
        }

        Int16Field IIsActiveRow.IsActiveField
        {
            get => fields.IsActive;
        }

        [DisplayName("IsAdmin")]
        public Boolean? IsAdmin { get => fields.IsAdmin[this]; set => fields.IsAdmin[this] = value; }

        public UserRow()
        {
        }

        public UserRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : LoggingRowFields
        {
            public Int32Field UserId;
            public StringField Username;
            public StringField Source;
            public StringField PasswordHash;
            public StringField PasswordSalt;
            public StringField DisplayName;
            public StringField Email;
            public StringField UserImage;
            public DateTimeField LastDirectoryUpdate;
            public Int16Field IsActive;

            public StringField Password;
            public StringField PasswordConfirm;
            public BooleanField IsAdmin;
        }

        [Description("Navigation"), ImplicitPermission(ReadPermissionKey)]
        public const string NavigationPermissionKey = "Administration:User:Navigation";
        [Description("View")]
        //public const string ReadPermissionKey = "Administration:User:Read";
        public const string ReadPermissionKey = "?";
        [Description("Insert")]
        public const string InsertPermissionKey = "Administration:User:Insert";
        [Description("Update")]
        public const string UpdatePermissionKey = "Administration:User:Update";
        [Description("Delete")]
        public const string DeletePermissionKey = "Administration:User:Delete";
    }
}