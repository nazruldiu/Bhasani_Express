using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace _Ext
{
    public partial class MaskedTimeEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "_Ext.MaskedTimeEditor";

        public MaskedTimeEditorAttribute()
            : base(Key)
        {
        }

        public bool ShowSeconds
        {
            get { return GetOption<bool>("ShowSeconds"); }
            set { SetOption("ShowSeconds", value); }
        }
    }
}
