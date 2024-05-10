
namespace BMS_Scheduler.BhasaniTask {
    import fld = CompanyInfoRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CompanyInfoDialog extends _Ext.DialogBase<CompanyInfoRow, any> {
        protected getFormKey() { return CompanyInfoForm.formKey; }
        protected getRowType() { return CompanyInfoRow; }
        protected getService() { return CompanyInfoService.baseUrl; }

        protected form = new CompanyInfoForm(this.idPrefix);

        constructor(options) {
            super(options);
        }

    }
}