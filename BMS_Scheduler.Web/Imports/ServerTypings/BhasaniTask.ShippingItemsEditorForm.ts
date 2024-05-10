namespace BMS_Scheduler.BhasaniTask {
    export interface ShippingItemsEditorForm {
        ShippingId: Serenity.IntegerEditor;
        DescriptionOfGoods: Serenity.StringEditor;
        Traiff: Serenity.StringEditor;
        Quantity: Serenity.IntegerEditor;
        Uom: Serenity.StringEditor;
        UnitRate: Serenity.DecimalEditor;
        TotalRate: Serenity.DecimalEditor;
    }

    export class ShippingItemsEditorForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.ShippingItemsEditor';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ShippingItemsEditorForm.init)  {
                ShippingItemsEditorForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.StringEditor;
                var w2 = s.DecimalEditor;

                Q.initFormType(ShippingItemsEditorForm, [
                    'ShippingId', w0,
                    'DescriptionOfGoods', w1,
                    'Traiff', w1,
                    'Quantity', w0,
                    'Uom', w1,
                    'UnitRate', w2,
                    'TotalRate', w2
                ]);
            }
        }
    }
}
