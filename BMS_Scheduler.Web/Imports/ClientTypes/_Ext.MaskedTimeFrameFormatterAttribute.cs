using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace _Ext
{
    public partial class MaskedTimeFrameFormatterAttribute : CustomFormatterAttribute
    {
        public const string Key = "_Ext.MaskedTimeFrameFormatter";

        public MaskedTimeFrameFormatterAttribute()
            : base(Key)
        {
        }
    }
}
