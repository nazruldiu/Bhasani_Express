
namespace BMS_Scheduler.BhasaniTask {
    import fld = CompanyInfoRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CompanyInfoGridEditor extends _Ext.GridEditorBase<CompanyInfoRow> {
        protected getColumnsKey() { return 'BhasaniTask.CompanyInfoEditor'; }
        protected getDialogType() { return CompanyInfoEditorDialog; }
        protected getLocalTextPrefix() { return CompanyInfoRow.localTextPrefix; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}