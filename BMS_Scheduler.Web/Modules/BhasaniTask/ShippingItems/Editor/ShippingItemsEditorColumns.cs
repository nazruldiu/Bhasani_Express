
namespace BMS_Scheduler.BhasaniTask.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BhasaniTask.ShippingItemsEditor")]
    [BasedOnRow(typeof(ShippingItemsRow), CheckNames = true)]
    public class ShippingItemsEditorColumns
    {
        [EditLink]
        public string DescriptionOfGoods { get; set; }
        public string Traiff { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public decimal UnitRate { get; set; }
        public decimal TotalRate { get; set; }
    }
}