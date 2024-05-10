/// <reference path="../Common/Helpers/LanguageList.ts" />

namespace BMS_Scheduler.ScriptInitialization {
    Q.Config.responsiveDialogs = true;
    Q.Config.rootNamespaces.push('BMS_Scheduler');
    Serenity.EntityDialog.defaultLanguageList = LanguageList.getValue;
    //Serenity.HtmlContentEditor.CKEditorBasePath = "~/Scripts/ckeditor/";

    q.DefaultEntityDialogOptions.ShowChangeLogButtonInToolbar = false;

    if ($.fn['colorbox']) {
        $.fn['colorbox'].settings.maxWidth = "95%";
        $.fn['colorbox'].settings.maxHeight = "95%";
    }

    window.onerror = Q.ErrorHandling.runtimeErrorHandler;
    Q.Config.rootNamespaces.push('_Ext');
}