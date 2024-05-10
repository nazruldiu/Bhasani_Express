namespace BMS_Scheduler.BhasaniTask {
    export interface CompanyInfoEditorForm {
        Name: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        WebSite: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Fax: Serenity.StringEditor;
        Logo: Serenity.StringEditor;
    }

    export class CompanyInfoEditorForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.CompanyInfoEditor';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CompanyInfoEditorForm.init)  {
                CompanyInfoEditorForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(CompanyInfoEditorForm, [
                    'Name', w0,
                    'Address', w0,
                    'WebSite', w0,
                    'Phone', w0,
                    'Fax', w0,
                    'Logo', w0
                ]);
            }
        }
    }
}
