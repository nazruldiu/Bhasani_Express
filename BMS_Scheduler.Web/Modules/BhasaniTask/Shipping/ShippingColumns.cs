using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask.Columns
{
    [ColumnsScript("BhasaniTask.Shipping")]
    [BasedOnRow(typeof(ShippingRow), CheckNames = true)]
    public class ShippingColumns
    {
        [EditLink]
        public string OrderNo { get; set; }
        public string InVoiceNo { get; set; }
        public DateTime InVoiceDate { get; set; }
        public string VesselOrFlightNo { get; set; }
        public string PackingSlipNo { get; set; }
        public int TotalBox { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalWeight { get; set; }
    }
}