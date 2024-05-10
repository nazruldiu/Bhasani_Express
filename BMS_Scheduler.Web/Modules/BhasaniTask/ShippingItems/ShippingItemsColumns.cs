using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask.Columns
{
    [ColumnsScript("BhasaniTask.ShippingItems")]
    [BasedOnRow(typeof(ShippingItemsRow), CheckNames = true)]
    public class ShippingItemsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public int Id { get; set; }
        public String ShippingOrderNo { get; set; }
        [EditLink]
        public string DescriptionOfGoods { get; set; }
        public string Traiff { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public decimal UnitRate { get; set; }
        public decimal TotalRate { get; set; }
    }
}