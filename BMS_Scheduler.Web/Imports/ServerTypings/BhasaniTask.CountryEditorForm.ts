namespace BMS_Scheduler.BhasaniTask {
    export interface CountryEditorForm {
        Name: Serenity.StringEditor;
    }

    export class CountryEditorForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.CountryEditor';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CountryEditorForm.init)  {
                CountryEditorForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(CountryEditorForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
