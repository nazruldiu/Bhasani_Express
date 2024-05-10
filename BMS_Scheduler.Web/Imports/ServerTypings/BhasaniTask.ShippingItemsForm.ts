namespace BMS_Scheduler.BhasaniTask {
    export interface ShippingItemsForm {
        DescriptionOfGoods: Serenity.StringEditor;
        Traiff: Serenity.StringEditor;
        Quantity: Serenity.IntegerEditor;
        Uom: Serenity.StringEditor;
        UnitRate: Serenity.DecimalEditor;
        TotalRate: Serenity.DecimalEditor;
    }

    export class ShippingItemsForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.ShippingItems';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ShippingItemsForm.init)  {
                ShippingItemsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.DecimalEditor;

                Q.initFormType(ShippingItemsForm, [
                    'DescriptionOfGoods', w0,
                    'Traiff', w0,
                    'Quantity', w1,
                    'Uom', w0,
                    'UnitRate', w2,
                    'TotalRate', w2
                ]);
            }
        }
    }
}
