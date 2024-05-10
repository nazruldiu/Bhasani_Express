using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace BMS_Scheduler.BhasaniTask.Columns
{
    [ColumnsScript("BhasaniTask.CompanyInfo")]
    [BasedOnRow(typeof(CompanyInfoRow), CheckNames = true)]
    public class CompanyInfoColumns
    {
        [EditLink]
        public string Name { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}