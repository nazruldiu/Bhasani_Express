using BMS_Scheduler.Billing;
using BMS_Scheduler.Billing.Endpoints;
using BMS_Scheduler.Setup;
using BMS_Scheduler.Setup.Endpoints;
using BMS_Scheduler.Task;
using BMS_Scheduler.Tax;
using BMS_Scheduler.Web.Modules.Common;
using BMS_Scheduler.Web.Modules.Common.Helpers;
using ExtensionMethods;
using NUglify.Helpers;
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
    public class BMSInvoiceGPRP
    {
        protected ISqlConnections SqlConnections;

        public BMSInvoiceGPRP()
        {
            this.SqlConnections = BMS_Scheduler.AppStatics.SqlConnections;
        }
        #region Billing Invoice
        public GPRPViewModel ProcessGPRPInvoice(IRequestContext Context, IDbConnection connection, GetInvoiceRequest getInvoiceRequest)
        {
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString(), Context));

            var dailyRundownList = new BmsUtility().GetDailyRundown(tvChannelId, Context, getInvoiceRequest);
            var gprpList = GetGPRPList(tvChannelId, Context, getInvoiceRequest).OrderBy(o => o.ClientContractGprpClientTvcId);

            //System.Diagnostics.Debug.AutoFlush = true;
            //dailyRundownList.ForEach(e => { System.Diagnostics.Debug.WriteLine(e.SegmentStartTime + " " + e.SegmentEndTime); });

            #region Get as per aired (time slot)
            List<BillingInvoiceDetailAsPerAiredRow> billingInvoiceDetailAsPerAireds = new List<BillingInvoiceDetailAsPerAiredRow>();
            gprpList.ForEach(gp =>
            {
                BillingInvoiceDetailAsPerAiredRow asPerAired = new BillingInvoiceDetailAsPerAiredRow();

                var broadcastedList = dailyRundownList.Where(d => d.SegmentStartTime.Value.TimeOfDay >= gp.StartDateTime.Value.TimeOfDay && d.SegmentStartTime.Value.TimeOfDay <= gp.EndDateTime.Value.TimeOfDay).ToList();

                asPerAired.Particulars = gp.StartEndString;
                asPerAired.SpotTvr = gp.SpotTvr;
                asPerAired.TotalSpot = broadcastedList.Count();
                asPerAired.TotalTvr = gp.SpotTvr * broadcastedList.Count();
                asPerAired.SpotRate = gp.SpotRate;
                asPerAired.Amount = gp.SpotRate * broadcastedList.Count();

                billingInvoiceDetailAsPerAireds.Add(asPerAired);
            });
            #endregion

            #region Other Addition (TODO: make this dynamic)
            List<BillingInvoiceDetailOtherAdditionRow> billingInvoiceDetailOtherAdditionRows = new List<BillingInvoiceDetailOtherAdditionRow>();
            BillingInvoiceDetailOtherAdditionRow billingInvoiceDetailOtherAdditionRow = new BillingInvoiceDetailOtherAdditionRow();

            //TODO: make dynamic
            if (getInvoiceRequest.IsVat)
            {
                var taxRate = new TaxRateRuleRetrieveHandler(Context).GetTaxRatePercent(connection);

                billingInvoiceDetailOtherAdditionRow.Particulars = "VAT";
                billingInvoiceDetailOtherAdditionRow.Percentage = taxRate;
                billingInvoiceDetailOtherAdditionRow.Amount = billingInvoiceDetailAsPerAireds.Sum(s => s.Amount) * (decimal)(taxRate / 100);

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
                billingInvoiceDetailOtherDeductionRow.Amount = billingInvoiceDetailAsPerAireds.Sum(s => s.Amount) * (decimal)0.15;

                billingInvoiceDetailOtherDeductionRows.Add(billingInvoiceDetailOtherDeductionRow);
            }
            #endregion

            #region Compare two lists TODO: there is always optios to refactor list comparison list
            BillingInvoiceDetailRow billingInvoiceDetailRow = new BillingInvoiceDetailRow();

            List<GprpVM> gprpVMs = new List<GprpVM>();
            foreach (var g in gprpList)
            {
                var dailyBroadcustedList = dailyRundownList.Where(d => d.SegmentStartTime >= g.StartDateTime && d.SegmentEndTime <= g.EndDateTime).ToList();

                var groupedList = dailyBroadcustedList.GroupBy(x => x.TvcId).Select(gVM => new GprpVM
                {
                    TVCDuration = gVM.Sum(s => s.NotMappedTVCDuration) + (gVM.Sum(s => s.NotMappedTVCFrame) / AppStatics.MaxFrame),
                    TVCFrame = gVM.Sum(s => s.NotMappedTVCFrame),
                    GRPs = g.TotalTvr,
                    ClientTVCId = g.ClientContractGprpClientTvcId,
                    BDT = g.TotalTvr * (gVM.Sum(s => s.NotMappedTVCDuration) + (gVM.Sum(s => s.NotMappedTVCFrame) / AppStatics.MaxFrame)) * (g.SpotRate / 10),
                    RemainingFrame = gVM.Sum(s => s.NotMappedTVCFrame) % AppStatics.MaxFrame
                });

                gprpVMs.AddRange(groupedList);
            }
            billingInvoiceDetailRow.Bdt = billingInvoiceDetailAsPerAireds.Sum(s => s.Amount);//gprpVMs.Sum(s => s.BDT);//finalBDT;
            billingInvoiceDetailRow.GrPs = billingInvoiceDetailAsPerAireds.Sum(s => s.TotalTvr);//gprpVMs.Sum(s => s.GRPs);
            billingInvoiceDetailRow.Acd = gprpVMs.Sum(s => s.TVCDuration);
            #endregion

            var invoiceDetails = new List<BillingInvoiceDetailRow>();
            invoiceDetails.Add(billingInvoiceDetailRow);

            return new GPRPViewModel()
            {
                InvoiceDetails = invoiceDetails,
                AsPerAireds = billingInvoiceDetailAsPerAireds,
                GPRPAsPerAireds = billingInvoiceDetailAsPerAireds,
                OtherAdditions = billingInvoiceDetailOtherAdditionRows,
                OtherDeductions = billingInvoiceDetailOtherDeductionRows
            };
        }
        public List<ClientContractGprpDetailRow> GetGPRPList(long tvChannelId, IRequestContext Context, GetInvoiceRequest req)
        {
            var fldClientTvc = ClientTvcRow.Fields.As("ClientTVC");
            var fldClientContractGPRPDetail = ClientContractGprpDetailRow.Fields.As("ClientContractGPRPDetail");
            var fldClientContractGPRP = ClientContractGprpRow.Fields.As("ClientContractGPRP");
            var fldClientContract = ClientContractRow.Fields.As("fldClientContract");
            var sqlQuery = new SqlQuery();

            if (req.IsRecipientClient)
            {
                sqlQuery
                .From(fldClientContractGPRPDetail)
                .InnerJoin(fldClientContractGPRP, fldClientContractGPRPDetail.ClientContractGprpId == fldClientContractGPRP.Id)
                .InnerJoin(fldClientTvc, fldClientContractGPRP.ClientTvcId == fldClientTvc.Id)
                .InnerJoin(fldClientContract, fldClientContractGPRP.ClientContractId == fldClientContract.Id)
                .Where(fldClientContract.TvChannelId == tvChannelId && fldClientContractGPRP.EffectiveFrom <= req.FromDate.ToLongDateFormat()
                && fldClientContractGPRP.EffectiveTo >= req.ToDate.ToLongDateFormat()
                && fldClientTvc.ClientId == req.ClientId);
                if (req.TVCList.Any())
                {
                    sqlQuery.Where(fldClientTvc.Id.In(req.TVCList));
                }
                sqlQuery.Select("*, ClientTvc.Id AS ClientContractGprpClientTvcId");
            }
            else
            {
                sqlQuery
                .From(fldClientContractGPRPDetail)
                .InnerJoin(fldClientContractGPRP, fldClientContractGPRPDetail.ClientContractGprpId == fldClientContractGPRP.Id)
                .InnerJoin(fldClientTvc, fldClientContractGPRP.ClientTvcId == fldClientTvc.Id)
                .InnerJoin(fldClientContract, fldClientContractGPRP.ClientContractId == fldClientContract.Id)
                .Where(fldClientContract.TvChannelId == tvChannelId && fldClientContractGPRP.EffectiveFrom <= req.FromDate.ToLongDateFormat()
                && fldClientContractGPRP.EffectiveTo >= req.ToDate.ToLongDateFormat()
                && fldClientTvc.AgencyId == req.AgencyId);
                if (req.TVCList.Any())
                {
                    sqlQuery.Where(fldClientTvc.Id.In(req.TVCList));
                }
                sqlQuery.Select("*, ClientTvc.Id AS ClientContractGprpClientTvcId");
            }

            List<ClientContractGprpDetailRow> gprpDetails = new List<ClientContractGprpDetailRow>();
            using (var connection = SqlConnections.NewFor<ClientContractGprpDetailRow>())
            {
                var result = connection.Query<ClientContractGprpDetailRow>(sqlQuery).ToList();
                gprpDetails = result;
            }

            if (gprpDetails.Count() > 0)
                return gprpDetails;
            else
                throw new Exception("Client Contract not found for the given date range.");
        }
        public BillingInvoiceRow ConvertGPRPViewModelToBillingInvoice(GPRPViewModel model, GetInvoiceRequest bi)
        {
            bi.BillingInvoice.BillingInvoiceDetailList = model.InvoiceDetails;
            bi.BillingInvoice.BillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            bi.BillingInvoice.GPRPBillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            bi.BillingInvoice.BillingInvoiceDetailOtherAdditionList = model.OtherAdditions;
            bi.BillingInvoice.BillingInvoiceDetailOtherDeductionList = model.OtherDeductions;

            return bi.BillingInvoice;
        }
        public LetterGeneratedTemplateRow PrepareLetterGeneratedTemplateForGPRP(IRequestContext Context, IDbConnection connection, BillingInvoiceRow billing)
        {
            LetterGeneratedTemplateRow letterGT = new LetterGeneratedTemplateRow();

            var letterTemplDic = GetLetterTemplateByInvoiceTemplate(billing.InvoiceTemplate, Context, connection);
            string invoiceLetterTemp = letterTemplDic["Invoice"];
            string transLetterTemp = letterTemplDic["Transmission"];

            var gprpStr = ReplaceGPRPInvoiceTemp(invoiceLetterTemp, transLetterTemp, billing, connection);

            letterGT.BillingId = billing.Id;
            letterGT.InvoiceLetterTemplate = gprpStr["Invoice"];
            letterGT.TransmissionCertificateLetterTemplate = gprpStr["Transmission"];

            return letterGT;
        }
        private Dictionary<string, string> ReplaceGPRPInvoiceTemp(string gprpInv, string transInv, BillingInvoiceRow billing, IDbConnection connection)
        {
            #region Invoice
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString()));
            var TVChannelInfo = OrganizationInfoUtils.GetTVChannelInformation(connection);
            var fldClientTvc = ClientTvcRow.Fields.As("ClientTVC");
            var fldClient = ClientsRow.Fields.As("Clients");
            var fldRecClient = ClientsRow.Fields.As("RecClients");
            var fldRecAgency = AgenciesRow.Fields.As("Agencies");
            var fldAgency = AgenciesRow.Fields.As("RecAgencies");
            var fldClientContractGprp = ClientContractGprpRow.Fields.As("ClientContractGPRP");
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
                .InnerJoin(fldClientContractGprp, fldClientContractGprp.EffectiveFrom <= billing.FromDate.Value.ToLongDateFormat() && fldClientContractGprp.EffectiveTo >= billing.ToDate.Value.ToLongDateFormat())
                .Where(fldClient.TvChannelId == tvChannelId && fldClient.Id == billing.ClientId.Value)
                .Select("CASE WHEN RecClients.CompanyName = null THEN RecAgencies.CompanyName ELSE RecClients.CompanyName END AS RecName, CASE WHEN RecClients.BillAddress = null THEN RecAgencies.BillAddress ELSE RecClients.BillAddress END AS Too,\r\n ClientContractGPRP.ContractNumber ContractNo,\r\nClientContractGPRP.ContractDate,\r\nClients.CompanyName Company,\r\nAgencies.CompanyName Advisor,\r\nClientTVC.ProductName Brand,\r\nClientTVC.TVCName TVCOrProduct,\r\nClientTVC.Duration Duration");

            GPRPLetterViewModel gPRPLetterVM = new GPRPLetterViewModel();
            var result = connection.Query<GPRPLetterViewModel>(sqlQuery).ToList();
            gPRPLetterVM = result.FirstOrDefault();
            gPRPLetterVM.StartDate = billing.FromDate.ToLongDateFormat();
            gPRPLetterVM.EndDate = billing.ToDate.ToLongDateFormat();
            gPRPLetterVM.ContractOfTransmission = billing.FromDate.ToLongDateFormat() + " to " + billing.ToDate.ToLongDateFormat();
            gPRPLetterVM.Schedule = billing.Schedule;
            gPRPLetterVM.ACD = billing.BillingInvoiceDetailList.FirstOrDefault().Acd.ToString();
            gPRPLetterVM.GRP = billing.BillingInvoiceDetailList.FirstOrDefault().GrPs.ToString();
            gPRPLetterVM.BDT = billing.BillingInvoiceDetailList.FirstOrDefault().Bdt.ToString();
            gPRPLetterVM.GrossTotal = billing.BillingInvoiceDetailList.FirstOrDefault().GrPs.ToString();
            gPRPLetterVM.VAT = billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount.ToString();
            gPRPLetterVM.Total = Convert.ToString(billing.BillingInvoiceDetailList.FirstOrDefault().Bdt + billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount);
            gPRPLetterVM.Less = billing.BillingInvoiceDetailOtherDeductionList.Where(x => x.Particulars.Contains("Agency")).FirstOrDefault().Amount.ToString();
            gPRPLetterVM.GrandTotal = Convert.ToString(billing.BillingInvoiceDetailList.FirstOrDefault().Bdt + billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount - billing.BillingInvoiceDetailOtherDeductionList.Where(x => x.Particulars.Contains("Agency")).FirstOrDefault().Amount);
            gPRPLetterVM.InWords = DecimalNumberToWord.NumberToWords(Convert.ToDouble(billing.BillingInvoiceDetailList.FirstOrDefault().Bdt + billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount - billing.BillingInvoiceDetailOtherDeductionList.Where(x => x.Particulars.Contains("Agency")).FirstOrDefault().Amount));
            gPRPLetterVM.Logo = TVChannelInfo.Logo;
            gPRPLetterVM.Address = TVChannelInfo.Address;
            gPRPLetterVM.Email = TVChannelInfo.Email;
            gPRPLetterVM.phone = TVChannelInfo.Phone;
            gPRPLetterVM.TVChannelName = TVChannelInfo.Name;
            #endregion

            Dictionary<string, string> generatedTemplates = new Dictionary<string, string>();
            generatedTemplates.Add("Invoice", gprpInv.gprpTemplate(gPRPLetterVM));
            generatedTemplates.Add("Transmission", transInv.gprpTemplate(gPRPLetterVM));

            return generatedTemplates;
        }
        public Dictionary<string, string> GetLetterTemplateByInvoiceTemplate(Common.InvoiceTemplateType templateType, IRequestContext Context, IDbConnection connection)
        {
            LetterTemplateRow lTemplate = new LetterTemplateRow();
            switch (templateType)
            {
                case Common.InvoiceTemplateType.GPRP:
                    lTemplate = GetLetterTemplate(Common.InvoiceTemplateType.GPRP, Context, connection);
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
        public LetterTemplateRow GetLetterTemplate(Common.InvoiceTemplateType template, IRequestContext Context, IDbConnection connection)
        {
            return new Setup.LetterTemplateListHandler(Context).List(connection, new ListRequest()).Entities.Where(x => (int)x.TvcType == 2).SingleOrDefault(); //TODO: change later
        }
        #endregion
    }

    #region ViewModel
    public class GPRPViewModel
    {
        public List<BillingInvoiceDetailAsPerAiredRow> AsPerAireds { get; set; }
        public List<BillingInvoiceDetailAsPerAiredRow> GPRPAsPerAireds { get; set; }
        public List<BillingInvoiceDetailOtherAdditionRow> OtherAdditions { get; set; }
        public List<BillingInvoiceDetailOtherDeductionRow> OtherDeductions { get; set; }
        public List<BillingInvoiceDetailRow> InvoiceDetails { get; set; }
    }
    public class BillingInvoiceDetailAsPerAiredViewModel
    {
        public bool IsPick { get; set; }
        public string ParticulasName { get; set; }
        public decimal Rate { get; set; }
    }
    public class GprpVM
    {
        public long? ClientTVCId { get; set; }
        public decimal? TVCDuration { get; set; }
        public decimal? TVCFrame { get; set; }
        public decimal? RemainingFrame { get; set; }
        public decimal? GRPs { get; set; }
        public decimal? BDT { get; set; }
    }
    #endregion
}
