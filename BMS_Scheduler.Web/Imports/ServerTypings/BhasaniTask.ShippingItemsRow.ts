namespace BMS_Scheduler.BhasaniTask {
    export interface ShippingItemsRow {
        Id?: number;
        ShippingId?: number;
        DescriptionOfGoods?: string;
        Traiff?: string;
        Quantity?: number;
        Uom?: string;
        UnitRate?: number;
        TotalRate?: number;
        ShippingOrderNo?: string;
        ShippingInVoiceNo?: string;
        ShippingInVoiceDate?: string;
        ShippingConsignee?: string;
        ShippingTotalAmount?: number;
        ShippingTotalWeight?: number;
        ShippingVesselOrFlightNo?: string;
        ShippingPaymentTerms?: string;
        ShippingPackingSlipNo?: string;
        ShippingTotalBox?: number;
        ShippingCountryOfOriginOfGoods?: number;
        ShippingFinalDestination?: number;
        ShippingMeasure?: string;
    }

    export namespace ShippingItemsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'DescriptionOfGoods';
        export const localTextPrefix = 'BhasaniTask.ShippingItems';
        export const deletePermission = '?';
        export const insertPermission = '?';
        export const readPermission = '?';
        export const updatePermission = '?';

        export declare const enum Fields {
            Id = "Id",
            ShippingId = "ShippingId",
            DescriptionOfGoods = "DescriptionOfGoods",
            Traiff = "Traiff",
            Quantity = "Quantity",
            Uom = "Uom",
            UnitRate = "UnitRate",
            TotalRate = "TotalRate",
            ShippingOrderNo = "ShippingOrderNo",
            ShippingInVoiceNo = "ShippingInVoiceNo",
            ShippingInVoiceDate = "ShippingInVoiceDate",
            ShippingConsignee = "ShippingConsignee",
            ShippingTotalAmount = "ShippingTotalAmount",
            ShippingTotalWeight = "ShippingTotalWeight",
            ShippingVesselOrFlightNo = "ShippingVesselOrFlightNo",
            ShippingPaymentTerms = "ShippingPaymentTerms",
            ShippingPackingSlipNo = "ShippingPackingSlipNo",
            ShippingTotalBox = "ShippingTotalBox",
            ShippingCountryOfOriginOfGoods = "ShippingCountryOfOriginOfGoods",
            ShippingFinalDestination = "ShippingFinalDestination",
            ShippingMeasure = "ShippingMeasure"
        }
    }
}
