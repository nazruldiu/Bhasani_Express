namespace BMS_Scheduler.BhasaniTask {
    export interface ShippingRow {
        Id?: number;
        OrderNo?: string;
        InVoiceNo?: string;
        InVoiceDate?: string;
        Consignee?: string;
        TotalAmount?: number;
        TotalWeight?: number;
        VesselOrFlightNo?: string;
        PaymentTerms?: string;
        PackingSlipNo?: string;
        TotalBox?: number;
        CountryOfOriginOfGoods?: number;
        FinalDestination?: number;
        Measure?: string;
        ShippingItemList?: ShippingItemsRow[];
    }

    export namespace ShippingRow {
        export const idProperty = 'Id';
        export const nameProperty = 'OrderNo';
        export const localTextPrefix = 'BhasaniTask.Shipping';
        export const deletePermission = 'BhasaniTask:Shipping:Delete';
        export const insertPermission = 'BhasaniTask:Shipping:Insert';
        export const readPermission = 'BhasaniTask:Shipping:Read';
        export const updatePermission = 'BhasaniTask:Shipping:Update';

        export declare const enum Fields {
            Id = "Id",
            OrderNo = "OrderNo",
            InVoiceNo = "InVoiceNo",
            InVoiceDate = "InVoiceDate",
            Consignee = "Consignee",
            TotalAmount = "TotalAmount",
            TotalWeight = "TotalWeight",
            VesselOrFlightNo = "VesselOrFlightNo",
            PaymentTerms = "PaymentTerms",
            PackingSlipNo = "PackingSlipNo",
            TotalBox = "TotalBox",
            CountryOfOriginOfGoods = "CountryOfOriginOfGoods",
            FinalDestination = "FinalDestination",
            Measure = "Measure",
            ShippingItemList = "ShippingItemList"
        }
    }
}
