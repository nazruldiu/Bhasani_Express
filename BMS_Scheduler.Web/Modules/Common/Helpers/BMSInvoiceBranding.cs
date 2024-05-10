using BMS_Scheduler.Billing;
using BMS_Scheduler.Billing.Endpoints;
using BMS_Scheduler.Setup;
using BMS_Scheduler.Setup.Endpoints;
using BMS_Scheduler.Task;
using BMS_Scheduler.Tax;
using BMS_Scheduler.Web.Modules.Common;
using BMS_Scheduler.Web.Modules.Common.Helpers;
using ExtensionMethods;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Org.BouncyCastle.Asn1.Ocsp;
using Serenity.Data;
using Serenity.Extensions.Entities;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static MVC.Views.Billing;

namespace BMS_Scheduler.Common.Helpers
{
    public class BMSInvoiceBranding
    {
        protected ISqlConnections SqlConnections;

        public BMSInvoiceBranding()
        {
            this.SqlConnections = BMS_Scheduler.AppStatics.SqlConnections;
        }
        public BrandingViewModel ProcessBrandingInvoice(IRequestContext Context, IDbConnection connection, GetInvoiceRequest getInvoiceRequest)
        {
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString(), Context));
            var brandingList = GetBrandingList(tvChannelId, Context, getInvoiceRequest);
            var dailyRundownList = new BmsUtility().GetDailyRundown(tvChannelId, Context, getInvoiceRequest);

            #region Compare two lists TODO: there is always optios to refactor list comparison list
            List<BillingInvoiceDetailRow> brandingDetails = new List<BillingInvoiceDetailRow>();
            List<BillingInvoiceDetailAsPerAiredRow> brandingAsPerAiredList = new List<BillingInvoiceDetailAsPerAiredRow>();

            foreach (var b in brandingList)
            {
                var list = new Setup.RateBasedOnListHandler(Context).List(connection, new ListRequest()).Entities;
                int episodeId = Convert.ToInt32(list.Where(x => x.Name.Contains("Episode")).FirstOrDefault().Id);
                int dayId = Convert.ToInt32(list.Where(x => x.Name.Contains("Day")).FirstOrDefault().Id);

                var dailyBroadcustedList = dailyRundownList.Where(d => d.SegmentStartTime >= b.StartDateTime && d.SegmentEndTime <= b.EndDateTime && d.TvcId == b.ClientTvcId).ToList();
                if (b.RateBasedOn == episodeId)
                {
                    BillingInvoiceDetailRow billingInvoiceDetail = new BillingInvoiceDetailRow();
                    BillingInvoiceDetailAsPerAiredRow brandingAsPerAired = new BillingInvoiceDetailAsPerAiredRow();

                    billingInvoiceDetail.Position = "Branding";
                    billingInvoiceDetail.NetRate = b.Rate;
                    billingInvoiceDetail.Episode = dailyBroadcustedList.Count();
                    billingInvoiceDetail.Bdt = billingInvoiceDetail.NetRate * billingInvoiceDetail.Episode;

                    brandingAsPerAired.Particulars = "Branding";
                    brandingAsPerAired.NetRate = b.Rate;
                    brandingAsPerAired.Day = dailyBroadcustedList.Count();
                    brandingAsPerAired.Amount = billingInvoiceDetail.NetRate * billingInvoiceDetail.Episode;

                    brandingDetails.Add(billingInvoiceDetail);
                    brandingAsPerAiredList.Add(brandingAsPerAired);
                }
                else if (b.RateBasedOn == dayId)
                {
                    BillingInvoiceDetailRow billingInvoiceDetail = new BillingInvoiceDetailRow();
                    BillingInvoiceDetailAsPerAiredRow brandingAsPerAired = new BillingInvoiceDetailAsPerAiredRow();

                    billingInvoiceDetail.Position = "Branding";
                    billingInvoiceDetail.NetRate = b.Rate;
                    billingInvoiceDetail.Episode = dailyBroadcustedList.GroupBy(x => x.RundownDate.ToDateFormat()).Count();
                    billingInvoiceDetail.Bdt = billingInvoiceDetail.NetRate * billingInvoiceDetail.Episode;

                    brandingAsPerAired.Particulars = "Branding";
                    brandingAsPerAired.NetRate = b.Rate;
                    brandingAsPerAired.Day = dailyBroadcustedList.GroupBy(x => x.RundownDate.ToDateFormat()).Count();
                    brandingAsPerAired.Amount = brandingAsPerAired.NetRate * brandingAsPerAired.Day;

                    brandingDetails.Add(billingInvoiceDetail);
                    brandingAsPerAiredList.Add(brandingAsPerAired);
                }

            }
            #region Other Addition (TODO: make this dynamic)
            List<BillingInvoiceDetailOtherAdditionRow> billingInvoiceDetailOtherAdditionRows = new List<BillingInvoiceDetailOtherAdditionRow>();
            BillingInvoiceDetailOtherAdditionRow billingInvoiceDetailOtherAdditionRow = new BillingInvoiceDetailOtherAdditionRow();

            //TODO: make dynamic
            if (getInvoiceRequest.IsVat)
            {
                var taxRate = new TaxRateRuleRetrieveHandler(Context).GetTaxRatePercent(connection);

                billingInvoiceDetailOtherAdditionRow.Particulars = "VAT";
                billingInvoiceDetailOtherAdditionRow.Percentage = taxRate; 
                billingInvoiceDetailOtherAdditionRow.Amount = brandingDetails.Sum(x => x.Bdt) * (decimal)(taxRate/100);

                billingInvoiceDetailOtherAdditionRows.Add(billingInvoiceDetailOtherAdditionRow);
            }
            #endregion

            #region Other Deduction (TODO: make this dynamic)
            List<BillingInvoiceDetailOtherDeductionRow> billingInvoiceDetailOtherDeductionRows = new List<BillingInvoiceDetailOtherDeductionRow>();
            BillingInvoiceDetailOtherDeductionRow billingInvoiceDetailOtherDeductionRow = new BillingInvoiceDetailOtherDeductionRow();

            if (getInvoiceRequest.IsAgencyCommission)
            {
                billingInvoiceDetailOtherDeductionRow.Particulars = "Agency Commission";
                billingInvoiceDetailOtherDeductionRow.Percentage = 15; 
                billingInvoiceDetailOtherDeductionRow.Amount = brandingDetails.Sum(x => x.Bdt) * (decimal)0.15;

                billingInvoiceDetailOtherDeductionRows.Add(billingInvoiceDetailOtherDeductionRow);
            }
            #endregion

            #endregion

            return new BrandingViewModel()
            {
                InvoiceDetails = brandingDetails,
                AsPerAireds = brandingAsPerAiredList,
                BrandingAsPerAireds = brandingAsPerAiredList,
                OtherAdditions = billingInvoiceDetailOtherAdditionRows,
                OtherDeductions = billingInvoiceDetailOtherDeductionRows
            }; 
        }

        public BillingInvoiceRow ConvertBrandingViewModelToBillingInvoice(BrandingViewModel model, GetInvoiceRequest bi)
        {
            bi.BillingInvoice.BillingInvoiceDetailList = model.InvoiceDetails;
            bi.BillingInvoice.BillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            bi.BillingInvoice.BrandingBillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            bi.BillingInvoice.BillingInvoiceDetailOtherAdditionList = model.OtherAdditions;
            bi.BillingInvoice.BillingInvoiceDetailOtherDeductionList = model.OtherDeductions;

            return bi.BillingInvoice;
        }

        public List<ClientContractBrandingDetailRow> GetBrandingList(long tvChannelId, IRequestContext Context, GetInvoiceRequest req)
        {
            //var tvcNames = string.Join(",", req.TVCName).Select(x => x.ToString()).ToList();
            var fldClientTvc = ClientTvcRow.Fields.As("ClientTVC");
            var fldClientContractBrandingDetail = ClientContractBrandingDetailRow.Fields.As("ClientContractBrandingDetail");
            var fldClientContractBranding = ClientContractBrandingRow.Fields.As("ClientContractBranding");
            var fldClientContract = ClientContractRow.Fields.As("fldClientContract");
            var sqlQuery = new SqlQuery();

            if (req.IsRecipientClient)
            {
                sqlQuery
                .From(fldClientContractBrandingDetail)
                .InnerJoin(fldClientContractBranding, fldClientContractBrandingDetail.ClientContractBrandingId == fldClientContractBranding.Id)
                .InnerJoin(fldClientTvc, fldClientContractBrandingDetail.ClientTvcId == fldClientTvc.Id)
                .InnerJoin(fldClientContract, fldClientContractBranding.ClientContractId == fldClientContract.Id)
                .Where(fldClientContract.TvChannelId == tvChannelId && fldClientContractBrandingDetail.StartDateTime >= req.FromDate.ToLongDateFormat()
                && fldClientContractBrandingDetail.EndDateTime <= req.ToDate.ToLongDateFormat()
                && fldClientTvc.ClientId == req.ClientId);
                if (req.TVCList.Any())
                {
                    sqlQuery.Where(fldClientContractBrandingDetail.ClientTvcId.In(req.TVCList));
                }
                sqlQuery.Select("*");
            }
            else
            {
                sqlQuery
                .From(fldClientContractBrandingDetail)
                .InnerJoin(fldClientContractBranding, fldClientContractBrandingDetail.ClientContractBrandingId == fldClientContractBranding.Id)
                .InnerJoin(fldClientTvc, fldClientContractBrandingDetail.ClientTvcId == fldClientTvc.Id)
                .InnerJoin(fldClientContract, fldClientContractBranding.ClientContractId == fldClientContract.Id)
                .Where(fldClientContract.TvChannelId == tvChannelId && fldClientContractBrandingDetail.StartDateTime >= req.FromDate.ToLongDateFormat()
                && fldClientContractBrandingDetail.EndDateTime <= req.ToDate.ToLongDateFormat()
                && fldClientTvc.AgencyId == req.AgencyId);
                if (req.TVCList.Any())
                {
                    sqlQuery.Where(fldClientContractBrandingDetail.ClientTvcId.In(req.TVCList));
                }
                sqlQuery.Select("*");
            }

            List<ClientContractBrandingDetailRow> BrandingDetails = new List<ClientContractBrandingDetailRow>();
            using (var connection = SqlConnections.NewFor<ClientContractBrandingDetailRow>())
            {
                var result = connection.Query<ClientContractBrandingDetailRow>(sqlQuery).ToList();
                BrandingDetails = result;
            }

            if (BrandingDetails.Count() > 0)
                return BrandingDetails;
            else
                throw new Exception("Client Contract not found for the given date range.");
        }

        public LetterGeneratedTemplateRow PrepareLetterGeneratedTemplateForBranding(IRequestContext Context, IDbConnection connection, BillingInvoiceRow billing)
        {
            LetterGeneratedTemplateRow letterGT = new LetterGeneratedTemplateRow();

            var letterTemplDic = GetLetterTemplateByInvoiceTemplate(billing.InvoiceTemplate, Context, connection);
            string invoiceLetterTemp = letterTemplDic["Invoice"];

            var gprpStr = ReplaceBrandingInvoiceTemp(invoiceLetterTemp, billing, connection);

            letterGT.BillingId = billing.Id;
            letterGT.InvoiceLetterTemplate = gprpStr["Invoice"];

            return letterGT;
        }

        private Dictionary<string, string> ReplaceBrandingInvoiceTemp(string gprpInv, BillingInvoiceRow billing, IDbConnection connection)
        {
            #region Invoice
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString()));
            var TVChannelInfo = OrganizationInfoUtils.GetTVChannelInformation(connection);
            var fldClientTvc = ClientTvcRow.Fields.As("ClientTVC");
            var fldClient = ClientsRow.Fields.As("Clients");
            var fldRecClient = ClientsRow.Fields.As("RecClients");
            var fldRecAgency = AgenciesRow.Fields.As("Agencies");
            var fldAgency = AgenciesRow.Fields.As("RecAgencies");
            var fldClientContractBranding = ClientContractBrandingRow.Fields.As("ClientContractBranding");
            var fldClientContract = ClientContractRow.Fields.As("ClientContract");

            int clientId = billing.ClientId != null ? billing.ClientId.Value : 0;
            int recipientClientId = billing.RecipientClientId != null ? billing.RecipientClientId.Value : 0;
            int recipientAgencyId = billing.RecipientClientId != null ? billing.RecipientClientId.Value : 0;
            int agencyId = billing.AgencyId != null ? billing.AgencyId.Value : 0;
            //int clientId = (int)billing.ClientId;

            var sqlQuery = new SqlQuery()
                .From(fldClient)
                .LeftJoin(fldRecClient, fldRecClient.Id == recipientClientId)
                .LeftJoin(fldRecAgency, fldRecAgency.Id == recipientAgencyId)
                .LeftJoin(fldAgency, fldAgency.Id == agencyId)
                .LeftJoin(fldClientTvc, fldClientTvc.ClientId == clientId)
                .LeftJoin(fldClientContract, fldClientContract.ClientId == clientId)
                .InnerJoin(fldClientContractBranding, fldClientContractBranding.EffectiveFrom >= billing.FromDate.Value && fldClientContractBranding.EffectiveFrom <= billing.ToDate.Value)
                .Where(fldClient.TvChannelId == tvChannelId && fldClient.Id == billing.ClientId.Value)
                .Select("CASE WHEN RecClients.CompanyName = null THEN RecAgencies.CompanyName ELSE RecClients.CompanyName END AS RecName, CASE WHEN RecClients.BillAddress = null THEN RecAgencies.BillAddress ELSE RecClients.BillAddress END AS Too,\r\n ClientContractBranding.ContractNumber ContractNo,\r\nClientContractBranding.ContractDate,\r\nClients.CompanyName Company,\r\nAgencies.CompanyName Advisor,\r\nClientTVC.ProductName Brand,\r\nClientTVC.TVCName TVCOrProduct,\r\nClientTVC.Duration Duration");

            BrandingLetterViewModel brandingLetterVM = new BrandingLetterViewModel();
            var result = connection.Query<BrandingLetterViewModel>(sqlQuery).ToList();

            string BillingDetailTable = "";
            string InitTable = "";
            string InitTransmission = "";
            if (billing.InvoiceTemplate == Common.InvoiceTemplateType.ProgramBranding)
            {
                InitTable = "<table style = 'padding-left:5px;font-size:14px;'><thead style='font-weight:bold;background-color:lightgray;'><tr><td style='width: 250px'>Position</td><td style='width: 150px'>Net Rate</td><td style='width: 150px'>Episode</td><td style='width: 100px;text-align: right'>BDT</td></tr></thead><tbody>";
                InitTransmission = "<table style = 'padding-left:5px;font-size:14px;'><thead style='font-weight:bold;background-color:lightgray;'><tr><td style='width: 250px'>Details</td><td style='width: 200px'></td><td style='width: 200px'>Min.</td></tr></thead><tbody>";
                foreach (var item in billing.BillingInvoiceDetailList)
                {
                    BillingDetailTable += $"<tr><td>{item.Position}</td><td>{item.NetRate}</td><td>{item.Episode}</td><td style='text-align: right;'>{item.Bdt}</td></tr>";
                    //BillingDetailTransmission += $"<tr><td>{item.Position}</td><td> - </td><td>{item.Min}</td></tr>";

                }

            }

            string BillingTableDesign = InitTable + BillingDetailTable + "</tbody></table>";
            brandingLetterVM.BillingDetail = BillingTableDesign;
            brandingLetterVM = result.FirstOrDefault();
            brandingLetterVM.StartDate = billing.FromDate.ToLongDateFormat();
            brandingLetterVM.EndDate = billing.ToDate.ToLongDateFormat();
            brandingLetterVM.ContractOfTransmission = billing.FromDate.ToLongDateFormat() + " to " + billing.ToDate.ToLongDateFormat();
            brandingLetterVM.Schedule = billing.Schedule;
            brandingLetterVM.BDT = billing.BillingInvoiceDetailList.Sum(x => x.Bdt).ToString();
            brandingLetterVM.BDTs = billing.BillingInvoiceDetailList.Sum(x => x.Bdt).ToString();
            brandingLetterVM.GrossTotal = billing.BillingInvoiceDetailList.FirstOrDefault().GrPs.ToString();
            brandingLetterVM.VAT = billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount.ToString();
            brandingLetterVM.Total = Convert.ToString(billing.BillingInvoiceDetailList.Sum(x => x.NetRate));
            brandingLetterVM.Episodes = Convert.ToString(billing.BillingInvoiceDetailList.Sum(x => x.Episode));
            brandingLetterVM.Less = billing.BillingInvoiceDetailOtherDeductionList.Where(x => x.Particulars.Contains("Agency")).FirstOrDefault().Amount.ToString();
            brandingLetterVM.GrandTotal = Convert.ToString(Convert.ToString(billing.BillingInvoiceDetailList.Sum(x => x.NetRate) + billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount));
            brandingLetterVM.InWords = DecimalNumberToWord.NumberToWords(Convert.ToDouble(billing.BillingInvoiceDetailList.Sum(x => x.NetRate)) + Convert.ToDouble(billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount));
            brandingLetterVM.Logo = TVChannelInfo.Logo;
            brandingLetterVM.Address = TVChannelInfo.Address;
            brandingLetterVM.Email = TVChannelInfo.Email;
            brandingLetterVM.phone = TVChannelInfo.Phone;
            brandingLetterVM.TVChannelName = TVChannelInfo.Name;
            brandingLetterVM.BillingDetail = BillingTableDesign;
            #endregion

            Dictionary<string, string> generatedTemplates = new Dictionary<string, string>();
            generatedTemplates.Add("Invoice", gprpInv.brandingTemplate(brandingLetterVM));

            return generatedTemplates;
        }

        public Dictionary<string, string> GetLetterTemplateByInvoiceTemplate(Common.InvoiceTemplateType templateType, IRequestContext Context, IDbConnection connection)
        {
            LetterTemplateRow lTemplate = new LetterTemplateRow();
            switch (templateType)
            {
                case Common.InvoiceTemplateType.ProgramBranding:
                    lTemplate = GetLetterTemplate(Common.InvoiceTemplateType.ProgramBranding, Context, connection);
                    break;
                default:
                    break;
            }

            Dictionary<string, string> templates = new Dictionary<string, string>();
            templates.Add("Invoice", lTemplate.InvoiceLetterTemplate); //adding a key/value using the Add() method
            templates.Add("Contract", lTemplate.ContractLetterTemplate);
            templates.Add("Transmission", lTemplate.TransmissionCertificateLetterTemplate);

            return templates;
        }
        //todo: fix hard coded part
        public LetterTemplateRow GetLetterTemplate(Common.InvoiceTemplateType template, IRequestContext Context, IDbConnection connection)
        {
            return new Setup.LetterTemplateListHandler(Context).List(connection, new ListRequest()).Entities.Where(x => (int)x.TvcType == 3).SingleOrDefault(); //TODO: change later
        }
    }

    #region ViewModel
    public class BrandingVM
    {
        public long? ClientTVCId { get; set; }
        public string Position { get; set; }
        public decimal? NetRate { get; set; }
        public int? Episode { get; set; }
        public decimal? BDT { get; set; }
    }
    public class BrandingViewModel
    {
        public List<BillingInvoiceDetailAsPerAiredRow> AsPerAireds { get; set; }
        public List<BillingInvoiceDetailAsPerAiredRow> BrandingAsPerAireds { get; set; }
        public List<BillingInvoiceDetailOtherAdditionRow> OtherAdditions { get; set; }
        public List<BillingInvoiceDetailOtherDeductionRow> OtherDeductions { get; set; }
        public List<BillingInvoiceDetailRow> InvoiceDetails { get; set; }
    }

    public class BrandingLetterViewModel
    {
        public string Too { get; set; }
        public string Schedule { get; set; }
        public string ContractNo { get; set; }
        public string ContractDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ContractOfTransmission { get; set; }
        public string Company { get; set; }
        public string Advisor { get; set; }
        public string Brand { get; set; }
        public string TVCOrProduct { get; set; }
        public string Duration { get; set; }
        public string ACD { get; set; }
        public string GRP { get; set; }
        public string BDT { get; set; }
        public string BDTs { get; set; }
        public string GrossTotal { get; set; }
        public string VAT { get; set; }
        public string Total { get; set; }
        public string Less { get; set; }
        public string GrandTotal { get; set; }
        public string InWords { get; set; }
        public string RecName { get; set; }
        public string NetTotal { get; set; }
        public string Episode { get; set; }
        public string Episodes { get; set; }
        public string Position { get; set; }
        public string ProgramName { get; set; }
        //Report Header
        public string Logo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string TVChannelName { get; set; }
        public string BillingDetail { get; internal set; }
        public List<BrandingBillingDetail> BillingDetailList { get; set; } = new List<BrandingBillingDetail>();
    }

    public class BrandingBillingDetail
    {
        public string Position { get; set; }
        public string NetRate { get; set; }
        public string Episode { get; set; }
        public string BDT { get; set; }
        //public string Day { get; set; }
    }
    #endregion
}
