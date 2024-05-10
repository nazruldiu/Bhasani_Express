﻿using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace _Ext
{
    public partial class TimePickerEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "_Ext.TimePickerEditor";

        public TimePickerEditorAttribute()
            : base(Key)
        {
        }
    }
}
