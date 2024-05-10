
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingItemsRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingItemsDialog extends _Ext.DialogBase<ShippingItemsRow, any> {
        protected getFormKey() { return ShippingItemsForm.formKey; }
        protected getRowType() { return ShippingItemsRow; }
        protected getService() { return ShippingItemsService.baseUrl; }

        protected form = new ShippingItemsForm(this.idPrefix);

        constructor(options) {
            super(options);
        }

    }
}