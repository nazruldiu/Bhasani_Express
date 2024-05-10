
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingItemsRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingItemsGrid extends _Ext.GridBase<ShippingItemsRow, any> {
        protected getColumnsKey() { return 'BhasaniTask.ShippingItems'; }
        protected getDialogType() { return ShippingItemsDialog; }
        protected getRowType() { return ShippingItemsRow; }
        protected getService() { return ShippingItemsService.baseUrl; }

        constructor(container: JQuery, options) {
            super(container, options);
        }
    }
}