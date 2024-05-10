
namespace BMS_Scheduler.BhasaniTask {
    import fld = CompanyInfoRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CompanyInfoEditorDialog extends _Ext.EditorDialogBase<CompanyInfoRow> {
        protected getFormKey() { return CompanyInfoEditorForm.formKey; }
        protected getLocalTextPrefix() { return CompanyInfoRow.localTextPrefix; }
        protected getNameProperty() { return CompanyInfoRow.nameProperty; }

        protected form = new CompanyInfoEditorForm(this.idPrefix);

    }
}