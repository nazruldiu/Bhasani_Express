﻿using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace _Ext.ExcelImporter
{
    public partial class ROExcelImportFieldMappingEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "_Ext.ExcelImporter.ROExcelImportFieldMappingEditor";

        public ROExcelImportFieldMappingEditorAttribute()
            : base(Key)
        {
        }
    }
}
