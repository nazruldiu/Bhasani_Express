namespace _Ext {
    export interface AuditLogForm {
        FeatureName: Serenity.StringEditor;
        EntityTableName: Serenity.StringEditor;
        ActionType: Serenity.EnumEditor;
        FormattedActionDate: Serenity.StringEditor;
        Changes: StaticTextBlock;
        UserId: Serenity.LookupEditor;
        IpAddress: Serenity.StringEditor;
        SessionId: Serenity.StringEditor;
        EntityId: Serenity.IntegerEditor;
    }

    export class AuditLogForm extends Serenity.PrefixedContext {
        static formKey = '_Ext.AuditLog';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AuditLogForm.init)  {
                AuditLogForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;
                var w2 = StaticTextBlock;
                var w3 = s.LookupEditor;
                var w4 = s.IntegerEditor;

                Q.initFormType(AuditLogForm, [
                    'FeatureName', w0,
                    'EntityTableName', w0,
                    'ActionType', w1,
                    'FormattedActionDate', w0,
                    'Changes', w2,
                    'UserId', w3,
                    'IpAddress', w0,
                    'SessionId', w0,
                    'EntityId', w4
                ]);
            }
        }
    }
}
