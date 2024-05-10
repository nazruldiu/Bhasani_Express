
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingGrid extends _Ext.GridBase<ShippingRow, any> {
        protected getColumnsKey() { return 'BhasaniTask.Shipping'; }
        protected getDialogType() { return ShippingDialog; }
        protected getRowType() { return ShippingRow; }
        protected getService() { return ShippingService.baseUrl; }

        constructor(container: JQuery, options) {
            super(container, options);
        }
    }
}