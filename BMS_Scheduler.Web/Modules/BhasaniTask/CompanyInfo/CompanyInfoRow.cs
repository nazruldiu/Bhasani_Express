using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask;

[ConnectionKey("Default"), TableName("[dbo].[CompanyInfo]")]
[DisplayName("Company Info"), InstanceName("Company Info"), TwoLevelCached]
[NavigationPermission("BhasaniTask:CompanyInfo:Navigation")]
[ReadPermission("BhasaniTask:CompanyInfo:Read")]
[InsertPermission("BhasaniTask:CompanyInfo:Insert")]
[UpdatePermission("BhasaniTask:CompanyInfo:Update")]
[DeletePermission("BhasaniTask:CompanyInfo:Delete")]
public sealed class CompanyInfoRow : Row<CompanyInfoRow.RowFields>, IIdRow, INameRow
{

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => Fields.Id[this]; set => Fields.Id[this] = value; }
	public partial class RowFields { public Int32Field Id; }

    [DisplayName("Name"), Size(100), QuickSearch, NameProperty]
    public string Name { get => Fields.Name[this]; set => Fields.Name[this] = value; }
	public partial class RowFields { public StringField Name; }

    [DisplayName("Address"), Size(400)]
    public string Address { get => Fields.Address[this]; set => Fields.Address[this] = value; }
	public partial class RowFields { public StringField Address; }

    [DisplayName("Web Site"), Size(100)]
    public string WebSite { get => Fields.WebSite[this]; set => Fields.WebSite[this] = value; }
	public partial class RowFields { public StringField WebSite; }

    [DisplayName("Phone"), Size(100)]
    public string Phone { get => Fields.Phone[this]; set => Fields.Phone[this] = value; }
	public partial class RowFields { public StringField Phone; }

    [DisplayName("Fax"), Size(100)]
    public string Fax { get => Fields.Fax[this]; set => Fields.Fax[this] = value; }
	public partial class RowFields { public StringField Fax; }

    [DisplayName("Logo"), Size(200)]
    public string Logo { get => Fields.Logo[this]; set => Fields.Logo[this] = value; }
	public partial class RowFields { public StringField Logo; }

    #region Foreign Fields

    #endregion Foreign Fields

    public CompanyInfoRow() : base() { }

    public CompanyInfoRow(RowFields fields) : base(fields) { }

	public partial class RowFields : RowFieldsBase { }
}
