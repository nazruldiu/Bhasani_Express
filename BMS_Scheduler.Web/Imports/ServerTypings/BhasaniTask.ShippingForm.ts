namespace BMS_Scheduler.BhasaniTask {
    export interface ShippingForm {
        OrderNo: Serenity.StringEditor;
        InVoiceNo: Serenity.StringEditor;
        InVoiceDate: Serenity.DateEditor;
        Consignee: Serenity.StringEditor;
        VesselOrFlightNo: Serenity.StringEditor;
        PaymentTerms: Serenity.StringEditor;
        PackingSlipNo: Serenity.StringEditor;
        CountryOfOriginOfGoods: Serenity.LookupEditor;
        FinalDestination: Serenity.LookupEditor;
        ShippingItemList: ShippingItemsGridEditor;
        TotalAmount: Serenity.DecimalEditor;
        TotalWeight: Serenity.DecimalEditor;
        TotalBox: Serenity.IntegerEditor;
        Measure: Serenity.StringEditor;
    }

    export class ShippingForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.Shipping';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ShippingForm.init)  {
                ShippingForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.LookupEditor;
                var w3 = ShippingItemsGridEditor;
                var w4 = s.DecimalEditor;
                var w5 = s.IntegerEditor;

                Q.initFormType(ShippingForm, [
                    'OrderNo', w0,
                    'InVoiceNo', w0,
                    'InVoiceDate', w1,
                    'Consignee', w0,
                    'VesselOrFlightNo', w0,
                    'PaymentTerms', w0,
                    'PackingSlipNo', w0,
                    'CountryOfOriginOfGoods', w2,
                    'FinalDestination', w2,
                    'ShippingItemList', w3,
                    'TotalAmount', w4,
                    'TotalWeight', w4,
                    'TotalBox', w5,
                    'Measure', w0
                ]);
            }
        }
    }
}
