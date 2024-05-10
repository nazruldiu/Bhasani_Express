
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingItemsRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingItemsEditorDialog extends _Ext.EditorDialogBase<ShippingItemsRow> {
        protected getFormKey() { return ShippingItemsEditorForm.formKey; }
        protected getLocalTextPrefix() { return ShippingItemsRow.localTextPrefix; }
        protected getNameProperty() { return ShippingItemsRow.nameProperty; }

        protected form = new ShippingItemsEditorForm(this.idPrefix);

    }
}