using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask.Forms
{
    [FormScript("BhasaniTask.ShippingItems")]
    [BasedOnRow(typeof(ShippingItemsRow), CheckNames = true)]
    public class ShippingItemsForm
    {
        public string DescriptionOfGoods { get; set; }
        public string Traiff { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public decimal UnitRate { get; set; }
        public decimal TotalRate { get; set; }
    }
}