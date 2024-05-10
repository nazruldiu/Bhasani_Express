using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask;

[ConnectionKey("Default"), TableName("[dbo].[Shipping]")]
[DisplayName("Shipping"), InstanceName("Shipping"), TwoLevelCached]
[NavigationPermission("BhasaniTask:Shipping:Navigation")]
[ReadPermission("BhasaniTask:Shipping:Read")]
[InsertPermission("BhasaniTask:Shipping:Insert")]
[UpdatePermission("BhasaniTask:Shipping:Update")]
[DeletePermission("BhasaniTask:Shipping:Delete")]
public sealed class ShippingRow : Row<ShippingRow.RowFields>, IIdRow, INameRow
{

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => Fields.Id[this]; set => Fields.Id[this] = value; }
	public partial class RowFields { public Int32Field Id; }

    [DisplayName("Order No."), Size(100), QuickSearch, NameProperty]
    public string OrderNo { get => Fields.OrderNo[this]; set => Fields.OrderNo[this] = value; }
	public partial class RowFields { public StringField OrderNo; }

    [DisplayName("Invoice No."), Size(100)]
    public string InVoiceNo { get => Fields.InVoiceNo[this]; set => Fields.InVoiceNo[this] = value; }
	public partial class RowFields { public StringField InVoiceNo; }

    [DisplayName("Invoice Date")]
    public DateTime? InVoiceDate { get => Fields.InVoiceDate[this]; set => Fields.InVoiceDate[this] = value; }
	public partial class RowFields { public DateTimeField InVoiceDate; }

    [DisplayName("Consignee"), Size(400)]
    public string Consignee { get => Fields.Consignee[this]; set => Fields.Consignee[this] = value; }
	public partial class RowFields { public StringField Consignee; }

    [DisplayName("Total Amount"), Size(18), Scale(2)]
    public decimal? TotalAmount { get => Fields.TotalAmount[this]; set => Fields.TotalAmount[this] = value; }
	public partial class RowFields { public DecimalField TotalAmount; }

    [DisplayName("Total Weight"), Size(18), Scale(2)]
    public decimal? TotalWeight { get => Fields.TotalWeight[this]; set => Fields.TotalWeight[this] = value; }
	public partial class RowFields { public DecimalField TotalWeight; }

    [DisplayName("Vessel Or Flight No"), Size(100)]
    public string VesselOrFlightNo { get => Fields.VesselOrFlightNo[this]; set => Fields.VesselOrFlightNo[this] = value; }
	public partial class RowFields { public StringField VesselOrFlightNo; }

    [DisplayName("Payment Terms"), Size(100)]
    public string PaymentTerms { get => Fields.PaymentTerms[this]; set => Fields.PaymentTerms[this] = value; }
	public partial class RowFields { public StringField PaymentTerms; }

    [DisplayName("Packing Slip No"), Size(100)]
    public string PackingSlipNo { get => Fields.PackingSlipNo[this]; set => Fields.PackingSlipNo[this] = value; }
	public partial class RowFields { public StringField PackingSlipNo; }

    [DisplayName("Total Box")]
    public int? TotalBox { get => Fields.TotalBox[this]; set => Fields.TotalBox[this] = value; }
	public partial class RowFields { public Int32Field TotalBox; }

    [DisplayName("Country Of Origin Of Goods")]
    [LookupEditor(typeof(CountryRow), InplaceAdd = true)]
    public int? CountryOfOriginOfGoods { get => Fields.CountryOfOriginOfGoods[this]; set => Fields.CountryOfOriginOfGoods[this] = value; }
	public partial class RowFields { public Int32Field CountryOfOriginOfGoods; }

    [DisplayName("Final Destination")]
    [LookupEditor(typeof(CountryRow), InplaceAdd = true)]
    public int? FinalDestination { get => Fields.FinalDestination[this]; set => Fields.FinalDestination[this] = value; }
	public partial class RowFields { public Int32Field FinalDestination; }

    [DisplayName("Measure"), Size(100)]
    public string Measure { get => Fields.Measure[this]; set => Fields.Measure[this] = value; }
	public partial class RowFields { public StringField Measure; }

    [DisplayName("Goods List"), NotMapped, MasterDetailRelation(foreignKey: "ShippingId")]
    public List<ShippingItemsRow> ShippingItemList { get { return Fields.ShippingItemList[this]; } set { Fields.ShippingItemList[this] = value; } }
    public partial class RowFields { public ListField<ShippingItemsRow> ShippingItemList; }

    #region Foreign Fields

    #endregion Foreign Fields

    public ShippingRow() : base() { }

    public ShippingRow(RowFields fields) : base(fields) { }

	public partial class RowFields : RowFieldsBase { }
}
