using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BMS_Scheduler
{
    public abstract class NRow<TFields> : Row<TFields>, IInsertLogRow, IUpdateLogRow, _Ext.IAuditLog
        where TFields : NRow<TFields>.NRowFields
    {

        [DisplayName("Serial"), NotMapped]
        public int? RowNum { get { return Fields.RowNum[this]; } set { Fields.RowNum[this] = value; } }

        [DisplayName("Insert Date")]
        public DateTime? IDate { get { return Fields.IDate[this]; } set { Fields.IDate[this] = value; } }

        [DisplayName("Insert User Id"), ForeignKey("[Users]", "Id"), LeftJoin("jIUser")]
        public string IUser { get { return Fields.IUser[this]; } set { Fields.IUser[this] = value; } }

        [DisplayName("Update Date")]
        public DateTime? EDate { get { return Fields.EDate[this]; } set { Fields.EDate[this] = value; } }

        [DisplayName("Update User Id"), ForeignKey("[Users]", "Id"), LeftJoin("jEUser")]
        public string EUser { get { return Fields.EUser[this]; } set { Fields.EUser[this] = value; } }


        public Field InsertUserIdField => Fields.IUser;
        public DateTimeField InsertDateField => Fields.IDate;

        public Field UpdateUserIdField => Fields.EUser;
        public DateTimeField UpdateDateField => Fields.EDate;

        protected NRow() : base() { }
        public NRow(TFields fields) : base(fields) { }


        public abstract class NRowFields : RowFieldsBase
        {
            public Int32Field RowNum;
            public StringField IUser;
            public StringField EUser;
            public DateTimeField IDate;
            public DateTimeField EDate;

            public NRowFields(string tableName = null, string fieldPrefix = "") : base(tableName, fieldPrefix)
            {

            }
        }

    }
}