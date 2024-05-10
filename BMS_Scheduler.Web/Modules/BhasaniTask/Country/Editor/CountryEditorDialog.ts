
namespace BMS_Scheduler.BhasaniTask {
    import fld = CountryRow.Fields;

    @Serenity.Decorators.registerClass()
    export class CountryEditorDialog extends _Ext.EditorDialogBase<CountryRow> {
        protected getFormKey() { return CountryEditorForm.formKey; }
        protected getLocalTextPrefix() { return CountryRow.localTextPrefix; }
        protected getNameProperty() { return CountryRow.nameProperty; }

        protected form = new CountryEditorForm(this.idPrefix);

    }
}