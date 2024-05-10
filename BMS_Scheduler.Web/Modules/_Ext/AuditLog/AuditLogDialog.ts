/// <reference path="../Bases/DialogBase.ts" />

namespace _Ext {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class AuditLogDialog extends DialogBase<AuditLogRow, any> {
        protected getFormKey() { return AuditLogForm.formKey; }
        protected getRowType() { return AuditLogRow; }
        protected getService() { return AuditLogService.baseUrl; }

        protected form = new AuditLogForm(this.idPrefix);

        constructor() {
            super();
            this.setReadOnly(true);
        }

        protected afterLoadEntity() {
            super.afterLoadEntity();

            this.form.Changes.value = AuditLogDialog.getChangesInHtml(this.entity.Changes);
            //this.setReadOnly(true);


        }



        static getChangesInHtml(changesInJson: string) {
            if (!changesInJson) return '';

            let changes = JSON.parse(changesInJson);

            let changesHtml = '';
            for (let field in changes) {
                let fieldValues = changes[field];
                changesHtml += `
<tr>
    <td>${field}</td>
    <td>${fieldValues[0]}</td>
    <td>${fieldValues[1]}</td>
</tr>
`;
            }

            return `
<table class="table table-bordered table-condensed table-striped">
    <tr>
        <th style="text-align:center;">Field</th>
        <th style="text-align:center;">Old Value</th>
        <th style="text-align:center;">New Value</th>
    </tr>
    ${changesHtml}
</table>
`;
        }
    }
}