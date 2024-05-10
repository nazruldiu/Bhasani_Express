namespace BMS_Scheduler.BhasaniTask {
    export interface CountryRow {
        Id?: number;
        Name?: string;
    }

    export namespace CountryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'BhasaniTask.Country';
        export const lookupKey = 'BhasaniTask:Country';

        export function getLookup(): Q.Lookup<CountryRow> {
            return Q.getLookup<CountryRow>('BhasaniTask:Country');
        }
        export const deletePermission = '?';
        export const insertPermission = '?';
        export const readPermission = '?';
        export const updatePermission = '?';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name"
        }
    }
}
