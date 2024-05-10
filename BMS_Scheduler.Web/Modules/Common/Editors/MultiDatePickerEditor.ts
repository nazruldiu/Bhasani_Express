namespace _Ext {
    @Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly])
    @Serenity.Decorators.editor()
    @Serenity.Decorators.element('<input type="text" autocomplete="off" class="form-control visually-hidden"/>')
    export class MultiDatePickerEditor extends Serenity.Widget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        public getEditValue(property, target) { target[property.name] = this.value; }
        public setEditValue(source, property) { this.value = source[property.name]; }

        private $container: JQuery;

        constructor($input: JQuery) {
            super($input);
            this.$container = $(`<div class="input-group date">
                                
                                <div class="input-group-addon">
                                    <i style='font-size:23px;' class="fa fa-calendar"></i>
                                </div>
                            </div>`)
            $input.addClass('visually-hidden');
            $input.parent().append(this.$container);
            this.$container.prepend($input);

            usingBootstrapDatePicker();

            (<any>this.$container).BSdatepicker({
                multidate: true,
                format: 'dd-mm-yyyy',
                //startDate: '01/10/2022',
                //endDate: '07/10/2022',
                //autoclose: true,
                //language: 'da',
                enableOnReadonly: false
            });
        }

        destroy() {
            super.destroy();
        }

        toDate = (dateStr) => {
            const [day, month, year] = dateStr.split("-")
            return new Date(year, month - 1, day)
        }

        public get value(): string {
            //let val = this.element.val();
            //console.log(val);
            //let strDates = val?.split(',');
            //let dates = [];
            //strDates.forEach(dt => {
            //    dates.push(this.toDate(dt));
            //});

            //return dates.join(',');
            return this.element.val();
        }

        public set value(val: string) {
            //this.element.val(val);

            let strDates = val?.split(',');
            this.$container['BSdatepicker']("setDates", strDates);
        }

        get_readOnly(): boolean {
            return this.element.hasClass('readonly');
        }

        set_readOnly(value: boolean): void {
            if (value == true) {
                this.element.addClass('readonly')
                    .attr("disabled", "disabled");
            } else {
                this.element.removeClass('readonly')
                    .removeAttr("disabled");
            }
        }

        set_minValue(value: string): void {
            this.element['BSdatepicker']("setStartDate", Q.parseISODateTime(value));
        }

        set_maxValue(value: string): void {
            this.element['BSdatepicker']("setEndDate", Q.parseISODateTime(value));
        }

    }
}