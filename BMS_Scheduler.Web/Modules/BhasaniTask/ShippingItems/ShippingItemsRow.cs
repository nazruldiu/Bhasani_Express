using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask;

[ConnectionKey("Default"), TableName("[dbo].[ShippingItems]")]
[DisplayName("Shipping Items"), InstanceName("Shipping Items"), TwoLevelCached]
[NavigationPermission("?")]
[ReadPermission("?")]
[InsertPermission("?")]
[UpdatePermission("?")]
[DeletePermission("?")]
public sealed class ShippingItemsRow : Row<ShippingItemsRow.RowFields>, IIdRow, INameRow
{

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => Fields.Id[this]; set => Fields.Id[this] = value; }
	public partial class RowFields { public Int32Field Id; }

    [DisplayName("Shipping"), NotNull, ForeignKey("[dbo].[Shipping]", "Id"), LeftJoin("jShipping"), TextualField("ShippingOrderNo")]
    public int? ShippingId { get => Fields.ShippingId[this]; set => Fields.ShippingId[this] = value; }
	public partial class RowFields { public Int32Field ShippingId; }

    [DisplayName("Description Of Goods"), Size(400), NotNull, QuickSearch, NameProperty]
    public string DescriptionOfGoods { get => Fields.DescriptionOfGoods[this]; set => Fields.DescriptionOfGoods[this] = value; }
	public partial class RowFields { public StringField DescriptionOfGoods; }

    [DisplayName("Traiff"), Size(100)]
    public string Traiff { get => Fields.Traiff[this]; set => Fields.Traiff[this] = value; }
	public partial class RowFields { public StringField Traiff; }

    [DisplayName("Quantity")]
    public int? Quantity { get => Fields.Quantity[this]; set => Fields.Quantity[this] = value; }
	public partial class RowFields { public Int32Field Quantity; }

    [DisplayName("Uom"), Column("UOM"), Size(100)]
    public string Uom { get => Fields.Uom[this]; set => Fields.Uom[this] = value; }
	public partial class RowFields { public StringField Uom; }

    [DisplayName("Unit Rate"), Size(18), Scale(2)]
    public decimal? UnitRate { get => Fields.UnitRate[this]; set => Fields.UnitRate[this] = value; }
	public partial class RowFields { public DecimalField UnitRate; }

    [DisplayName("Total Rate"), Size(18), Scale(2)]
    public decimal? TotalRate { get => Fields.TotalRate[this]; set => Fields.TotalRate[this] = value; }
	public partial class RowFields { public DecimalField TotalRate; }

    #region Foreign Fields


    [DisplayName("Shipping Order No"), Expression("jShipping.[OrderNo]"), ReadOnly(true)]
    public string ShippingOrderNo { get => Fields.ShippingOrderNo[this]; set => Fields.ShippingOrderNo[this] = value; }
	public partial class RowFields { public StringField ShippingOrderNo; }

    [DisplayName("Shipping In Voice No"), Expression("jShipping.[InVoiceNo]"), ReadOnly(true)]
    public string ShippingInVoiceNo { get => Fields.ShippingInVoiceNo[this]; set => Fields.ShippingInVoiceNo[this] = value; }
	public partial class RowFields { public StringField ShippingInVoiceNo; }

    [DisplayName("Shipping In Voice Date"), Expression("jShipping.[InVoiceDate]"), ReadOnly(true)]
    public DateTime? ShippingInVoiceDate { get => Fields.ShippingInVoiceDate[this]; set => Fields.ShippingInVoiceDate[this] = value; }
	public partial class RowFields { public DateTimeField ShippingInVoiceDate; }

    [DisplayName("Shipping Consignee"), Expression("jShipping.[Consignee]"), ReadOnly(true)]
    public string ShippingConsignee { get => Fields.ShippingConsignee[this]; set => Fields.ShippingConsignee[this] = value; }
	public partial class RowFields { public StringField ShippingConsignee; }

    [DisplayName("Shipping Total Amount"), Expression("jShipping.[TotalAmount]"), ReadOnly(true)]
    public decimal? ShippingTotalAmount { get => Fields.ShippingTotalAmount[this]; set => Fields.ShippingTotalAmount[this] = value; }
	public partial class RowFields { public DecimalField ShippingTotalAmount; }

    [DisplayName("Shipping Total Weight"), Expression("jShipping.[TotalWeight]"), ReadOnly(true)]
    public decimal? ShippingTotalWeight { get => Fields.ShippingTotalWeight[this]; set => Fields.ShippingTotalWeight[this] = value; }
	public partial class RowFields { public DecimalField ShippingTotalWeight; }

    [DisplayName("Shipping Vessel Or Flight No"), Expression("jShipping.[VesselOrFlightNo]"), ReadOnly(true)]
    public string ShippingVesselOrFlightNo { get => Fields.ShippingVesselOrFlightNo[this]; set => Fields.ShippingVesselOrFlightNo[this] = value; }
	public partial class RowFields { public StringField ShippingVesselOrFlightNo; }

    [DisplayName("Shipping Payment Terms"), Expression("jShipping.[PaymentTerms]"), ReadOnly(true)]
    public string ShippingPaymentTerms { get => Fields.ShippingPaymentTerms[this]; set => Fields.ShippingPaymentTerms[this] = value; }
	public partial class RowFields { public StringField ShippingPaymentTerms; }

    [DisplayName("Shipping Packing Slip No"), Expression("jShipping.[PackingSlipNo]"), ReadOnly(true)]
    public string ShippingPackingSlipNo { get => Fields.ShippingPackingSlipNo[this]; set => Fields.ShippingPackingSlipNo[this] = value; }
	public partial class RowFields { public StringField ShippingPackingSlipNo; }

    [DisplayName("Shipping Total Box"), Expression("jShipping.[TotalBox]"), ReadOnly(true)]
    public int? ShippingTotalBox { get => Fields.ShippingTotalBox[this]; set => Fields.ShippingTotalBox[this] = value; }
	public partial class RowFields { public Int32Field ShippingTotalBox; }

    [DisplayName("Shipping Country Of Origin Of Goods"), Expression("jShipping.[CountryOfOriginOfGoods]"), ReadOnly(true)]
    public int? ShippingCountryOfOriginOfGoods { get => Fields.ShippingCountryOfOriginOfGoods[this]; set => Fields.ShippingCountryOfOriginOfGoods[this] = value; }
	public partial class RowFields { public Int32Field ShippingCountryOfOriginOfGoods; }

    [DisplayName("Shipping Final Destination"), Expression("jShipping.[FinalDestination]"), ReadOnly(true)]
    public int? ShippingFinalDestination { get => Fields.ShippingFinalDestination[this]; set => Fields.ShippingFinalDestination[this] = value; }
	public partial class RowFields { public Int32Field ShippingFinalDestination; }

    [DisplayName("Shipping Measure"), Expression("jShipping.[Measure]"), ReadOnly(true)]
    public string ShippingMeasure { get => Fields.ShippingMeasure[this]; set => Fields.ShippingMeasure[this] = value; }
	public partial class RowFields { public StringField ShippingMeasure; }


    #endregion Foreign Fields

    public ShippingItemsRow() : base() { }

    public ShippingItemsRow(RowFields fields) : base(fields) { }

	public partial class RowFields : RowFieldsBase { }
}
