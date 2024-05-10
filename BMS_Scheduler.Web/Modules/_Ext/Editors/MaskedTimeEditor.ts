namespace _Ext {
    @Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly])
    @Serenity.Decorators.editor()
    @Serenity.Decorators.element('<input type="text"/>')
    export class MaskedTimeEditor extends Serenity.Widget<MaskedTimeEditorOptions> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        public getEditValue(property, target) { target[property.name] = this.value; }
        public setEditValue(source, property) { this.value = source[property.name]; }

        constructor(input: JQuery, options?: MaskedTimeEditorOptions) {
            super(input, options);

            if (options.ShowSeconds == undefined)
                options.ShowSeconds = false;
            /*setTimeout(() => {*/
            /*($(input) as any).mask("99:99");*/
            ($(input) as any).inputmask("datetime", {
                inputFormat: options.ShowSeconds ? "HH:MM:ss" : "HH:MM",
                max: 24
            });
            /* });*/


            this.options = options;
        }

        public get value(): number {
            let inputVal = this.element.val();
            return this.ConvertToSecond(inputVal);
        }

        //protected get_value(): string;

        public set value(val: number) {
            //if (val) {

            //    this.element.val(this.convertSecondToHourMinute(val));
            //} else {
            //    this.element.val(val);
            //}

            this.element.val(this.ConvertToTimeString(val));
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


        private ConvertToSecond(val: string): number {
            let hourMinute = val.split(":");
            if (hourMinute.length >= 0) {
                let hour = q.ToNumber(hourMinute[0]);
                let minute = q.ToNumber(hourMinute[1]);
                let second = 0;
                if (hourMinute.length > 1)
                    second = q.ToNumber(hourMinute[2]);
                let totalSecond = (hour * 60 * 60) + (minute * 60) + second;

                return totalSecond;
            }
        }

        private ConvertToTimeString(val: number): string {
            let myValue = Q.toId(val);
            let result = '';

            //if (myValue == 0) {
            //    return '12:00:00';
            //}

            if (myValue > 0) {
                let hourPart: number = 0;
                let minutePart: number = 0;
                let secondPart: number = 0;
                let inHour = myValue / 3600;
                hourPart = parseInt(inHour.toString());
                let remainBalance = inHour - hourPart;
                let minute = q.ToNumber((remainBalance * 60).toFixed(4));
                minutePart = parseInt(minute.toString());
                remainBalance = minute - minutePart;
                let second = q.ToNumber((remainBalance * 60).toFixed(4));
                if (second > 59) {
                    minutePart = minutePart + 1;
                    secondPart = 0;
                }
                else {
                    secondPart = Math.round(remainBalance * 60);
                }

                result = `${String('00' + hourPart).slice(-2)}:${String('00' + minutePart).slice(-2)}:${String('00' + secondPart).slice(-2)}`;
            }
            else
                result = null;

            return result;
        }

        //protected set_value(value: string): void;
    }

    export interface MaskedTimeEditorOptions {
        /*Use24HourFormat: boolean,*/
        ShowSeconds: boolean
    }

}