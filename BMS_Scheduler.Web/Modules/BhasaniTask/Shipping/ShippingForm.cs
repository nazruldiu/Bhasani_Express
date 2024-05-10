using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask.Forms
{
    [FormScript("BhasaniTask.Shipping")]
    [BasedOnRow(typeof(ShippingRow), CheckNames = true)]
    public class ShippingForm
    {
        [HalfWidth(UntilNext = true)]
        public string OrderNo { get; set; }
        public string InVoiceNo { get; set; }
        public DateTime InVoiceDate { get; set; }
        public string Consignee { get; set; }
        public string VesselOrFlightNo { get; set; }
        public string PaymentTerms { get; set; }
        public string PackingSlipNo { get; set; }
        public int CountryOfOriginOfGoods { get; set; }
        public int FinalDestination { get; set; }
        [ShippingItemsGridEditor,FullWidth]
        public List<ShippingItemsRow> ShippingItemList { get; set; }
        [HalfWidth(UntilNext = true)]
        public decimal TotalAmount { get; set; }
        public decimal TotalWeight { get; set; }
        public int TotalBox { get; set; }
        public string Measure { get; set; }
    }
}