
namespace BMS_Scheduler.BhasaniTask {
    import fld = CountryRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CountryGridEditor extends _Ext.GridEditorBase<CountryRow> {
        protected getColumnsKey() { return 'BhasaniTask.CountryEditor'; }
        protected getDialogType() { return CountryEditorDialog; }
        protected getLocalTextPrefix() { return CountryRow.localTextPrefix; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}