
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingItemsRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingItemsGridEditor extends _Ext.GridEditorBase<ShippingItemsRow> {
        protected getColumnsKey() { return 'BhasaniTask.ShippingItemsEditor'; }
        protected getDialogType() { return ShippingItemsEditorDialog; }
        protected getLocalTextPrefix() { return ShippingItemsRow.localTextPrefix; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}