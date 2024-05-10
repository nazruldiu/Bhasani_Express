
namespace BMS_Scheduler.BhasaniTask.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BhasaniTask.ShippingEditor")]
    [BasedOnRow(typeof(ShippingRow), CheckNames = true)]
    public class ShippingEditorColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public int Id { get; set; }
        [EditLink]
        public string OrderNo { get; set; }
        public string InVoiceNo { get; set; }
        public DateTime InVoiceDate { get; set; }
        public string Consignee { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalWeight { get; set; }
        public string VesselOrFlightNo { get; set; }
        public string PaymentTerms { get; set; }
        public string PackingSlipNo { get; set; }
        public int TotalBox { get; set; }
        public int CountryOfOriginOfGoods { get; set; }
        public int FinalDestination { get; set; }
        public string Measure { get; set; }
    }
}