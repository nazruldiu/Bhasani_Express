namespace _Ext {
    @Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly])
    @Serenity.Decorators.editor()
    @Serenity.Decorators.element(`<input type="text"/>`)
    export class ColorEditor extends Serenity.Widget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        public getEditValue(property, target) { target[property.name] = this.value; }
        public setEditValue(source, property) { this.value = source[property.name]; }

        constructor(input: JQuery) {
            super(input);

            usingBootstrapColorPicker();
            (<any>input).colorpicker({ default: '#FFFFFF' });
            //(<any>input).on('colorpickerChange', (event) => {
            //    //if (event.color)
            //    //    (<any>input).css('background-color', event.color.toString());

            //    //if (event.value === input.val() || !event.color || !event.color.isValid()) {
            //    //    // do not replace the input value if the color is invalid or equals
            //    //    return;
            //    //}
            //    input.val(event.color.string());
            //});

            //(<any>input).on('change keyup', () => {
            //    if (input.val())
            //        (<any>input).colorpicker("setValue", input.val());
            //});
        }

        public get value(): number {
            let colorValue = this.element.val();
            //if (colorValue)
            //    this.element.css({ 'background': colorValue });
            return colorValue;
        }

        public set value(val: number) {
            this.element.val(val);
        }


        get_readOnly(): boolean {
            return this.element.hasClass('readonly');
        }

        set_readOnly(value: boolean): void {
            if (value == true) {
                this.element.addClass('readonly');
                this.element.attr("readonly", "readonly");
            } else {
                this.element.removeClass('readonly');
                this.element.removeAttr("readonly");
            }
        }
    }
}
