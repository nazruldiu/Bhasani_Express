
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingEditorDialog extends _Ext.EditorDialogBase<ShippingRow> {
        protected getFormKey() { return ShippingEditorForm.formKey; }
        protected getLocalTextPrefix() { return ShippingRow.localTextPrefix; }
        protected getNameProperty() { return ShippingRow.nameProperty; }

        protected form = new ShippingEditorForm(this.idPrefix);

    }
}