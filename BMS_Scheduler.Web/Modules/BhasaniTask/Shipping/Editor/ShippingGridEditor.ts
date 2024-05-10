
namespace BMS_Scheduler.BhasaniTask {
    import fld = ShippingRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ShippingGridEditor extends _Ext.GridEditorBase<ShippingRow> {
        protected getColumnsKey() { return 'BhasaniTask.ShippingEditor'; }
        protected getDialogType() { return ShippingEditorDialog; }
        protected getLocalTextPrefix() { return ShippingRow.localTextPrefix; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}