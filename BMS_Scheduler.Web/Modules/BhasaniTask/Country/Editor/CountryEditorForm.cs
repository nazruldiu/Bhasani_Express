
namespace BMS_Scheduler.BhasaniTask.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BhasaniTask.CountryEditor")]
    [BasedOnRow(typeof(CountryRow), CheckNames = true)]
    public class CountryEditorForm
    {
        public string Name { get; set; }
    }
}