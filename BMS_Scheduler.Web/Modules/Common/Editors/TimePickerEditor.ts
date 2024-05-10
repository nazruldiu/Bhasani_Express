namespace _Ext {
    @Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue])
    @Serenity.Decorators.editor()
    @Serenity.Decorators.element("<div/>")
    export class TimePickerEditor extends Serenity.TemplatedWidget<any>
        implements Serenity.IGetEditValue, Serenity.ISetEditValue {
        protected getTemplate() {
            usingBootstrapTimePicker()

            return `
                        <div>
                            <input class="form-control timePicker" type="text" />
                        </div>
                    `;
        };


        constructor(container: JQuery) {
            super(container);
            try {
                (<any>this.element).timePicker({
                    format: 'hh:mm:ss a'
                });
            } catch (e) { }
        }

        public getEditValue(property, target) {
            try {
                let editVal = this.element.find('input.timePicker').val();
                target[property.name] = editVal;
            } catch (e) { }
        }

        public setEditValue(source, property) {
            let val = source[property.name];
            //this.element.children('input').val(val);
            try {
                this.element.data('timePicker').setValue(val);
            } catch (e) { }
        }

    }
}