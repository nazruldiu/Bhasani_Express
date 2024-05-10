using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace BMS_Scheduler.BhasaniTask
{
    public partial class CountryGridEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "BMS_Scheduler.BhasaniTask.CountryGridEditor";

        public CountryGridEditorAttribute()
            : base(Key)
        {
        }
    }
}
