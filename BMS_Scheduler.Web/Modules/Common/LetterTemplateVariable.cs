using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS_Scheduler
{
    public class LetterTemplateVariable
    {
        public const string AllInCSV
            = TVChannelLogo + "," + ReceipientName + "," + ReceipientAddress + "," + ContractNo + "," + IssueDate + "," + ContractDate + "," + StartDate + "," + EndDate
              + "," + InvoiceNo + "," + ContractTransmission + "," + Company + "," + Advertiser + "," + Product + "," + TVCDuration + "," + Schedule
              + "," + Position + "," + NetRate +"," + Min + "," + Amount + "," + MinNetTotal + "," + BDTNetTotal+ ","  + Vat + "," + GrandTotal + "," + MinGrandTotal + "," + GrandTotalWord
              + "," + PreparedBySignature + "," + CheckedBySignature + ","+ AuthorisedSignature+ "," + MarketingSalesHeadSignature + "," + TVChanneName + "," + TVChannelAddress + "," + Email
              + "," + Contact + "," + Website + "," + PRFNo + "," + Day +"," + BillingInvoiceDetail + "," + BillingInvoiceTransmissionDetail;
        
        public const string TVChannelLogo = "TV Channel Logo";
        public const string ReceipientName = "Receipient Name";
        public const string ReceipientAddress = "Receipient Address";
        public const string ContractNo = "Contract No.";
        public const string IssueDate = "Issue Date";
        public const string ContractDate = "Contract Date";
        public const string StartDate = "Start Date";
        public const string EndDate = "End Date";
        public const string InvoiceNo = "Invoice No.";
        public const string ContractTransmission = "Contract of Transmission";
        public const string Company = "Company";
        public const string Advertiser = "Advertiser";
        public const string Product = "TVC/Product";
        public const string TVCDuration = "TVC Duration";
        public const string Schedule = "Schedule";
        public const string Position = "Position";
        public const string NetRate = "Net Rate";
        public const string Min = "Min.";
        public const string Amount = "Amount";
        public const string MinNetTotal = "Min Net Total";
        public const string BDTNetTotal = "BDT Net Total";
        public const string Vat = "Add(%)VAT";
        public const string GrandTotal = "Grand Total";
        public const string MinGrandTotal = "Min. Grand Total";
        public const string GrandTotalWord = "Grand Total In word";
        public const string PreparedBySignature = "Prepared By Signature";
        public const string CheckedBySignature = "Checked By Signature";
        public const string AuthorisedSignature = "Authorised Signature";
        public const string MarketingSalesHeadSignature = "Head of Marketing & Sales Signature";
        public const string TVChanneName = "TVChannel Name";
        public const string TVChannelAddress = "TVChannel Address";
        public const string Email = "Email";
        public const string Contact = "Contact";
        public const string Website = "Website";
        public const string PRFNo = "PRF No.";
        public const string Day = "Day";
        public const string BillingInvoiceDetail = "Billing Invoice Detail";
        public const string BillingInvoiceTransmissionDetail = "Billing Invoice Transmission Detail";
        


    }
}
