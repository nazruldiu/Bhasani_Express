
namespace BMS_Scheduler.BhasaniTask {
    import fld = CompanyInfoRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CompanyInfoGrid extends _Ext.GridBase<CompanyInfoRow, any> {
        protected getColumnsKey() { return 'BhasaniTask.CompanyInfo'; }
        protected getDialogType() { return CompanyInfoDialog; }
        protected getRowType() { return CompanyInfoRow; }
        protected getService() { return CompanyInfoService.baseUrl; }

        constructor(container: JQuery, options) {
            super(container, options);
        }
    }
}