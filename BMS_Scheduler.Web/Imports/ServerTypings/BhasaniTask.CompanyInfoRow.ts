namespace BMS_Scheduler.BhasaniTask {
    export interface CompanyInfoRow {
        Id?: number;
        Name?: string;
        Address?: string;
        WebSite?: string;
        Phone?: string;
        Fax?: string;
        Logo?: string;
    }

    export namespace CompanyInfoRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'BhasaniTask.CompanyInfo';
        export const deletePermission = 'BhasaniTask:CompanyInfo:Delete';
        export const insertPermission = 'BhasaniTask:CompanyInfo:Insert';
        export const readPermission = 'BhasaniTask:CompanyInfo:Read';
        export const updatePermission = 'BhasaniTask:CompanyInfo:Update';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Address = "Address",
            WebSite = "WebSite",
            Phone = "Phone",
            Fax = "Fax",
            Logo = "Logo"
        }
    }
}
