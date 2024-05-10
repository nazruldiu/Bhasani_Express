
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingDialog extends _Ext.DialogBase<ShippingRow, any> {
        protected getFormKey() { return ShippingForm.formKey; }
        protected getRowType() { return ShippingRow; }
        protected getService() { return ShippingService.baseUrl; }

        protected form = new ShippingForm(this.idPrefix);

        constructor(options) {
            super(options);
        }

    }
}