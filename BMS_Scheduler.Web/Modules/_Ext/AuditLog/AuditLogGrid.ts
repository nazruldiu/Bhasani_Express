/// <reference path="../Bases/GridBase.ts" />

namespace _Ext {
    import fld = AuditLogRow.Fields
    @Serenity.Decorators.registerClass()
    export class AuditLogGrid extends GridBase<AuditLogRow, any> {
        protected getColumnsKey() { return '_Ext.AuditLog'; }
        protected getDialogType() { return AuditLogDialog; }
        protected getRowType() { return AuditLogRow; }
        protected getService() { return AuditLogService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        
        protected getButtons() {
            var buttons = super.getButtons();
            buttons.splice(0, 1);

            return buttons;
        }

        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[] {
            let filters = super.getQuickFilters();
            Q.first(filters, x => x.field == fld.ActionDate).init = w => {
                let firstDay = new Date();
                firstDay.setHours(0, 0, 0, 0);
                (w as Serenity.DateTimeEditor).valueAsDate = firstDay;
            };
            return filters;
        }
    }
}