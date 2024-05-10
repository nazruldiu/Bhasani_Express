using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask;

[ConnectionKey("Default"), TableName("[dbo].[Country]")]
[DisplayName("Country"), InstanceName("Country"), TwoLevelCached]
[NavigationPermission("BhasaniTask:Country:Navigation")]
[ReadPermission("?")]
[InsertPermission("?")]
[UpdatePermission("?")]
[DeletePermission("?")]
[LookupScript("BhasaniTask:Country", Expiration = -1)]
public sealed class CountryRow : Row<CountryRow.RowFields>, IIdRow, INameRow
{

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => Fields.Id[this]; set => Fields.Id[this] = value; }
	public partial class RowFields { public Int32Field Id; }

    [DisplayName("Name"), Size(100), QuickSearch, NameProperty]
    public string Name { get => Fields.Name[this]; set => Fields.Name[this] = value; }
	public partial class RowFields { public StringField Name; }

    #region Foreign Fields

    #endregion Foreign Fields

    public CountryRow() : base() { }

    public CountryRow(RowFields fields) : base(fields) { }

	public partial class RowFields : RowFieldsBase { }
}
