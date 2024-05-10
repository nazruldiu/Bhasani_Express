namespace _Ext {

    @Serenity.Decorators.registerFormatter([Serenity.ISlickFormatter])
    export class AuditLogActionTypeFormatter implements Slick.Formatter {
        static format(ctx: Slick.FormatterContext) {
            let item = ctx.item as AuditLogRow;

            let klass = '';
            if (item.ActionType == AuditActionType.Update) {
                klass = 'warning'
            } else if (item.ActionType == AuditActionType.Delete) {
                klass = 'danger'
            } else if (item.ActionType == AuditActionType.Insert) {
                klass = 'success'
            }
            else {
                klass = 'default'
            }

            return `<span style="border-radius:1px;" class="badge bg-${klass}">${AuditActionType[item.ActionType]}</span>`;
        }

        format(ctx: Slick.FormatterContext) {
            return AuditLogActionTypeFormatter.format(ctx);
        }
    }

}