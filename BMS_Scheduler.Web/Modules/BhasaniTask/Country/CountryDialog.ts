
namespace BMS_Scheduler.BhasaniTask {
    import fld = CountryRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CountryDialog extends _Ext.DialogBase<CountryRow, any> {
        protected getFormKey() { return CountryForm.formKey; }
        protected getRowType() { return CountryRow; }
        protected getService() { return CountryService.baseUrl; }

        protected form = new CountryForm(this.idPrefix);

        constructor(options) {
            super(options);
        }

    }
}