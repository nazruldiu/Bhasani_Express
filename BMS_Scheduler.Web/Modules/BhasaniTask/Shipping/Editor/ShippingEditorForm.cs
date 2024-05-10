
namespace BMS_Scheduler.BhasaniTask.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BhasaniTask.ShippingEditor")]
    [BasedOnRow(typeof(ShippingRow), CheckNames = true)]
    public class ShippingEditorForm
    {
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