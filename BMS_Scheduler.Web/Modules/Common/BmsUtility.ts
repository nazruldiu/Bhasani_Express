namespace BmsUtility {
    export function GetTime(value: number) {
        //if (value == 0)
        //    return '12:00:00';

        let hours = Math.floor(value / 3600);
        value %= 3600;
        let minutes = Math.floor(value / 60);
        let seconds = value % 60;
        // If you want strings with leading zeroes:
        minutes = pad('00', minutes.toString(), 2);
        hours = pad('00', hours.toString(), 2);
        seconds = pad('00', seconds.toString(), 2);

        //var calculatedTime = '';
        //if (value > 0)
        //    calculatedTime = hours + ":" + minutes + ":" + seconds;
        //else
        //    calculatedTime = '12:00:00';

        //hours = String(hours).padStart(2, "0");
        //seconds = String(seconds).padStart(2, "0");
        return hours + ":" + minutes + ":" + seconds;
    }

    export function GetTimeCode(value: number, frame: number) {
        //if (value == 0)
        //    return '12:00:00';

        let hours = Math.floor(value / 3600);
        value %= 3600;
        let minutes = Math.floor(value / 60);
        let seconds = value % 60;
        // If you want strings with leading zeroes:
        minutes = pad('00', minutes.toString(), 2);
        hours = pad('00', hours.toString(), 2);
        seconds = pad('00', seconds.toString(), 2);
        let frameStr = pad('00', frame.toString(), 2);

        //var calculatedTime = '';
        //if (value > 0)
        //    calculatedTime = hours + ":" + minutes + ":" + seconds;
        //else
        //    calculatedTime = '12:00:00';

        //hours = String(hours).padStart(2, "0");
        //seconds = String(seconds).padStart(2, "0");
        return `${hours}:${minutes}:${seconds}:${frameStr}`;
        //return hours + ":" + minutes + ":" + seconds;
    }

    export function pad(pad, str, padLeft) {
        if (typeof str === 'undefined')
            return pad;
        if (padLeft) {
            return (pad + str).slice(-pad.length);
        } else {
            return (str + pad).substring(0, pad.length);
        }
    }

    export function ConvertToSecond(val: string): number {
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

    export function getLongDate(val: Date) {
        let month = val.toLocaleString('en-us', { month: 'long' });
        let year = val.getFullYear();
        let day = val.getDate();
        return pad('00', day.toString(), 2) + "-" + month + "-" + year;
    }

    //export function getCategoryName(id: number) {
    //    let res = "";
    //    BMS_Scheduler.Setup.ProgramCategoryService.Retrieve({ EntityId: id }, r => {
    //        res = r.Entity.Category;
    //    }, { async: false });

    //    return res;
    //}

    export function getDates(start: Date, end: Date) {
        for (var arr = [], dt = new Date(start.toISOString()); dt <= new Date(end.toISOString()); dt.setDate(dt.getDate() + 1)) {
            arr.push(new Date(dt.toISOString()));
        }
        return arr;
    }

    export function getDatesFromString(commaSepDates: string) {
        let datesStringArray = commaSepDates.split(',');
        let dateArray = datesStringArray.map(date => {
            var dateParts = date.split("-");

            return new Date(new Date(Number(dateParts[2]), Number(dateParts[1]) - 1, +Number(dateParts[0])).toISOString()); 
        });

        return dateArray;
    }

    export function CombineDateAndTime(date: Date, timeString: string) {
        var year = date.getFullYear();
        var month = date.getMonth() + 1; // Jan is 0, dec is 11
        var day = date.getDate();
        var dateString = '' + year + '-' + month + '-' + day;
        var combined = new Date(dateString + ' ' + timeString);
        var res = new Date(combined.getTime() - combined.getTimezoneOffset() * 60000).toISOString();

        return res;
    };
}