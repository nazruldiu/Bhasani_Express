
namespace BMS_Scheduler.BhasaniTask {
    import fld = CountryRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CountryGrid extends _Ext.GridBase<CountryRow, any> {
        protected getColumnsKey() { return 'BhasaniTask.Country'; }
        protected getDialogType() { return CountryDialog; }
        protected getRowType() { return CountryRow; }
        protected getService() { return CountryService.baseUrl; }

        constructor(container: JQuery, options) {
            super(container, options);
        }
    }
}