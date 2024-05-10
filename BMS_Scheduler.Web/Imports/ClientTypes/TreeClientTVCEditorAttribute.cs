using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace BMS_Scheduler
{
    public partial class TreeClientTVCEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "BMS_Scheduler.TreeClientTVCEditor";

        public TreeClientTVCEditorAttribute()
            : base(Key)
        {
        }
    }
}
