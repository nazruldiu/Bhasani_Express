namespace _Ext {

    @Serenity.Decorators.registerFormatter([Serenity.ISlickFormatter])
    export class MaskedTimeFrameFormatter implements Slick.Formatter {
        static format(val: string) {
            let myValue = q.ToNumber(val);

            if (myValue >= 0) {
                return BmsUtility.GetTime(q.ToNumber(val));
                //let splitedTimeFrame = val.split(':');
                //let frame = 0;
                //if (splitedTimeFrame.length > 1)
                //    frame = q.ToNumber(splitedTimeFrame[1]);
                //let myValue = q.ToNumber(splitedTimeFrame[0]);

                //if (myValue > 0) {
                //    let hourPart: number = 0;
                //    let minutePart: number = 0;
                //    let secondPart: number = 0;
                //    let inHour = myValue / 3600;
                //    hourPart = parseInt(inHour.toString());
                //    let remainBalance = inHour - hourPart;
                //    let minute = q.ToNumber((remainBalance * 60).toFixed(4));
                //    minutePart = parseInt(minute.toString());
                //    remainBalance = minute - minutePart;
                //    let second = q.ToNumber((remainBalance * 60).toFixed(4));
                //    if (second > 59) {
                //        minutePart = minutePart + 1;
                //        secondPart = 0;
                //    }
                //    else {
                //        secondPart = Math.round(remainBalance * 60);
                //    }
                //    return `${String('00' + hourPart).slice(-2)}:${String('00' + minutePart).slice(-2)}:${String('00' + secondPart).slice(-2)}:${String('00' + frame).slice(-2)}`;
                //}
            }
           
            else return '';
        }

        format(ctx: Slick.FormatterContext) {
            return MaskedTimeFrameFormatter.format(ctx.value);
        }
    }

}