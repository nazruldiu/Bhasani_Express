namespace BMS_Scheduler.BhasaniTask {
    export interface CompanyInfoForm {
        Name: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        WebSite: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Fax: Serenity.StringEditor;
        Logo: Serenity.StringEditor;
    }

    export class CompanyInfoForm extends Serenity.PrefixedContext {
        static formKey = 'BhasaniTask.CompanyInfo';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CompanyInfoForm.init)  {
                CompanyInfoForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(CompanyInfoForm, [
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
