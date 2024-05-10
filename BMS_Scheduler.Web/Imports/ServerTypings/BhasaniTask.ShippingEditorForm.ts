namespace BMS_Scheduler.BhasaniTask {
    export interface ShippingEditorForm {
        OrderNo: Serenity.StringEditor;
        InVoiceNo: Serenity.StringEditor;
        InVoiceDate: Serenity.DateEditor;
        Consignee: Serenity.StringEditor;
        TotalAmount: Serenity.DecimalEditor;
        TotalWeight: Serenity.DecimalEditor;
        VesselOrFlightNo: Serenity.StringEditor;
        PaymentTerms: Serenity.StringEditor;
        PackingSlipNo: Serenity.StringEditor;
        TotalBox: Serenity.IntegerEditor;
        CountryOfOriginOfGoods: Serenity.LookupEditor;
        FinalDestination: Serenity.LookupEditor;
        Measure: Serenity.StringEditor;
    }

    export class ShippingEditorForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.ShippingEditor';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ShippingEditorForm.init)  {
                ShippingEditorForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.LookupEditor;

                Q.initFormType(ShippingEditorForm, [
                    'OrderNo', w0,
                    'InVoiceNo', w0,
                    'InVoiceDate', w1,
                    'Consignee', w0,
                    'TotalAmount', w2,
                    'TotalWeight', w2,
                    'VesselOrFlightNo', w0,
                    'PaymentTerms', w0,
                    'PackingSlipNo', w0,
                    'TotalBox', w3,
                    'CountryOfOriginOfGoods', w4,
                    'FinalDestination', w4,
                    'Measure', w0
                ]);
            }
        }
    }
}
