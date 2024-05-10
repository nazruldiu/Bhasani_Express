using BMS_Scheduler.Billing;
using BMS_Scheduler.Billing.Endpoints;
using BMS_Scheduler.Setup;
using BMS_Scheduler.Setup.Endpoints;
using BMS_Scheduler.Task;
using BMS_Scheduler.Tax;
using BMS_Scheduler.Web.Modules.Common;
using BMS_Scheduler.Web.Modules.Common.Helpers;
using ExtensionMethods;
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

namespace BMS_Scheduler.Common.Helpers
{
    public class BMSInvoiceRegular
    {
        protected ISqlConnections SqlConnections;

        public BMSInvoiceRegular()
        {
            this.SqlConnections = BMS_Scheduler.AppStatics.SqlConnections;
        }
        public RegularViewModel ProcessRegularInvoice(IRequestContext Context, IDbConnection connection, GetInvoiceRequest getInvoiceRequest)
        {
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString(), Context));
            var dailyRundownList = new BmsUtility().GetDailyRundown(tvChannelId, Context, getInvoiceRequest);
            var regularList = GetRegularList(tvChannelId, Context, getInvoiceRequest);
            var programCategoryList = GetProgramCategory(tvChannelId);

            #region InProgress:: Calculation and Set Billing Invoice Detail And As Per Aired
            List<BillingInvoiceDetailRow> billingInvoiceDetailList = new List<BillingInvoiceDetailRow>();

            List<BillingInvoiceDetailAsPerAiredRow> billingInvoiceDetailAsPerAiredList = new List<BillingInvoiceDetailAsPerAiredRow>();

            foreach (var regular in regularList)
            {
                var regularBroadCastList = dailyRundownList.Where(rundown => rundown.StartTime >= regular.StartTime && rundown.EndTime <= regular.EndTime && rundown.FPCProgramCategoryId == regular.ProgramCategoryId && rundown.SegmentPositionCommercialPositionId == regular.CommercialPositionId).ToList();

                var asPerAired = regularBroadCastList.GroupBy(q => q.TimeSlotName).Select(s => new BillingInvoiceDetailAsPerAiredRow
                {
                    Second = (int)(s.Sum(r => r.NotMappedTVCDuration) + (s.Sum(r => r.NotMappedTVCFrame) / AppStatics.MaxSecond)),
                    ExtraFrameMin = (int)(s.Sum(r => r.NotMappedTVCFrame) % AppStatics.MaxFrame),
                    Minutes = ((s.Sum(r => r.NotMappedTVCFrame) / AppStatics.MaxSecond) + (s.Sum(r => r.NotMappedTVCDuration))) / AppStatics.MaxSecond,
                    Particulars = s.Key,
                    NetRate = getInvoiceRequest.IsRateforMinutes ? regular.RateForMinutes : regular.RateForSpot,
                    Amount = Q.Round((((s.Sum(r => r.NotMappedTVCFrame) / AppStatics.MaxSecond) + (s.Sum(r => r.NotMappedTVCDuration))) / AppStatics.MaxSecond) * (getInvoiceRequest.IsRateforMinutes ? regular.RateForMinutes : regular.RateForSpot))
                }).ToList();


                var billingInvoiceDetail = regularBroadCastList.GroupBy(q => new { q.TimeSlotName, q.FPCProgramCategoryId }).Select(s => new BillingInvoiceDetailRow
                {
                    Second = (int)(s.Sum(r => r.NotMappedTVCDuration) + (s.Sum(r => r.NotMappedTVCFrame) / AppStatics.MaxSecond)),
                    Min = ((s.Sum(r => r.NotMappedTVCFrame) / AppStatics.MaxSecond) + (s.Sum(r => r.NotMappedTVCDuration))) / AppStatics.MaxSecond,
                    Position = getInvoiceRequest.ParticularsId == Common.InvoiceParticularsBrakingType.TimeSlot ? s.Key.TimeSlotName : getInvoiceRequest.ParticularsId == Common.InvoiceParticularsBrakingType.ProgramCategory ? programCategoryList.Where(x => x.Id == s.Key.FPCProgramCategoryId).Select(s => s.Category).FirstOrDefault() : s.Key.TimeSlotName + " " + programCategoryList.Where(x => x.Id == s.Key.FPCProgramCategoryId).Select(s => s.Category).FirstOrDefault(),
                    NetRate = getInvoiceRequest.IsRateforMinutes ? regular.RateForMinutes : regular.RateForSpot,
                    Bdt = Q.Round((((s.Sum(r => r.NotMappedTVCFrame) / AppStatics.MaxSecond) + (s.Sum(r => r.NotMappedTVCDuration))) / AppStatics.MaxSecond) * (getInvoiceRequest.IsRateforMinutes ? regular.RateForMinutes : regular.RateForSpot))

                }).ToList();

                #region InProgress:: Set As Per Aired                                    

                billingInvoiceDetailAsPerAiredList.AddRange(asPerAired);
                #endregion

                #region Inprogress:: Set Billing Invoice Detail

                billingInvoiceDetailList.AddRange(billingInvoiceDetail);
                #endregion

            }

            #endregion

            #region InProgress:: Set Other Addition
            List<BillingInvoiceDetailOtherAdditionRow> billingInvoiceDetailOtherAdditionList = new List<BillingInvoiceDetailOtherAdditionRow>();
            BillingInvoiceDetailOtherAdditionRow billingInvoiceDetailOtherAddition = new BillingInvoiceDetailOtherAdditionRow();
            if (getInvoiceRequest.IsVat)
            {
                var taxRate = new TaxRateRuleRetrieveHandler(Context).GetTaxRatePercent(connection);
                billingInvoiceDetailOtherAddition.Particulars = "VAT";
                billingInvoiceDetailOtherAddition.Percentage = taxRate;
                billingInvoiceDetailOtherAddition.Amount = (int)((billingInvoiceDetailAsPerAiredList.Sum(s => s.Amount) * taxRate) / 100);
                billingInvoiceDetailOtherAdditionList.Add(billingInvoiceDetailOtherAddition);
            }

            #endregion

            #region InProgress:: Set Other Deduction
            List<BillingInvoiceDetailOtherDeductionRow> billingInvoiceDetailOtherDeductionList = new List<BillingInvoiceDetailOtherDeductionRow>();
            BillingInvoiceDetailOtherDeductionRow OtherDeductionAgency = new BillingInvoiceDetailOtherDeductionRow();
            BillingInvoiceDetailOtherDeductionRow OtherDeductionDiscount = new BillingInvoiceDetailOtherDeductionRow();
            if (getInvoiceRequest.IsAgencyCommission)
            {
                var agencyCommission = 0;
                if (getInvoiceRequest.AgencyId != 0)
                {
                    agencyCommission = new AgenciesRetrieveHandler(Context).GetAgencyCommission(connection, getInvoiceRequest.AgencyId);

                }
                OtherDeductionAgency.Particulars = "Agency Commission";
                OtherDeductionAgency.Percentage = agencyCommission;
                OtherDeductionAgency.Amount = (int)((billingInvoiceDetailAsPerAiredList.Sum(s => s.Amount) * agencyCommission) / 100);
                billingInvoiceDetailOtherDeductionList.Add(OtherDeductionAgency);
            }
            else
            {
                OtherDeductionAgency.Particulars = "Agency Commission";
                OtherDeductionAgency.Percentage = 0;
                OtherDeductionAgency.Amount = 0;
                billingInvoiceDetailOtherDeductionList.Add(OtherDeductionAgency);
            }

            OtherDeductionDiscount.Particulars = "Discount";
            OtherDeductionDiscount.Percentage = getInvoiceRequest.Discount;
            OtherDeductionDiscount.Amount = (int)((billingInvoiceDetailAsPerAiredList.Sum(s => s.Amount) * getInvoiceRequest.Discount) / 100);
            billingInvoiceDetailOtherDeductionList.Add(OtherDeductionDiscount);
            #endregion

            return new RegularViewModel()
            {
                InvoiceDetails = billingInvoiceDetailList,
                AsPerAireds = billingInvoiceDetailAsPerAiredList,
                GPRPAsPerAireds = billingInvoiceDetailAsPerAiredList,
                OtherAdditions = billingInvoiceDetailOtherAdditionList,
                OtherDeductions = billingInvoiceDetailOtherDeductionList
            };

        }
        public BillingInvoiceRow ConvertRegularViewModelToBillingInvoice(RegularViewModel model, GetInvoiceRequest invoice)
        {
            invoice.BillingInvoice.BillingInvoiceDetailList = model.InvoiceDetails;
            invoice.BillingInvoice.BillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            invoice.BillingInvoice.GPRPBillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            invoice.BillingInvoice.BillingInvoiceDetailOtherAdditionList = model.OtherAdditions;
            invoice.BillingInvoice.BillingInvoiceDetailOtherDeductionList = model.OtherDeductions;

            return invoice.BillingInvoice;
        }
        public List<ClientContractRegularDetailRow> GetRegularList(long tvChannelId, IRequestContext Context, GetInvoiceRequest req)
        {
            //var tvcNames = string.Join(",", req.TVCName).Select(x => x.ToString()).ToList();
            var fldClientContractRegularDetail = ClientContractRegularDetailRow.Fields.As("fldClientContractRegularDetail");
            var fldClientContractRegular = ClientContractRegularRow.Fields.As("fldClientContractRegular");
            var fldClientContract = ClientContractRow.Fields.As("fldClientContract");

            var sqlQuery = new SqlQuery()
                .From(fldClientContractRegularDetail)
                .InnerJoin(fldClientContractRegular, fldClientContractRegularDetail.ClientContractRegularId == fldClientContractRegular.Id)
                .InnerJoin(fldClientContract, fldClientContractRegular.ClientContractId == fldClientContract.Id)
                .Where(fldClientContractRegular.EffectiveFrom <= req.FromDate.ToLongDateFormat()
                && fldClientContractRegular.EffectiveTo >= req.ToDate.ToLongDateFormat()
                && fldClientContract.TvChannelId == tvChannelId
                && fldClientContract.ClientId == req.ClientId)
                .Select("*");

            List<ClientContractRegularDetailRow> RegularDetails = new List<ClientContractRegularDetailRow>();
            using (var connection = SqlConnections.NewFor<ClientContractRegularDetailRow>())
            {
                var result = connection.Query<ClientContractRegularDetailRow>(sqlQuery).ToList();
                RegularDetails = result;
            }

            return RegularDetails;
        }
        public List<ProgramCategoryRow> GetProgramCategory(long tvChannelId)
        {
            var fldProgramCategory = ProgramCategoryRow.Fields.As("fldProgramCategory");
            var query = new SqlQuery()
                .Select(fldProgramCategory.Id)
                .Select(fldProgramCategory.Category)
                .From(fldProgramCategory)
                .Where(fldProgramCategory.TvChannelId == tvChannelId);
            List<ProgramCategoryRow> ProgramCategoryList = new List<ProgramCategoryRow>();
            using (var connection = SqlConnections.NewFor<ProgramCategoryRow>())
            {
                var result = connection.Query<ProgramCategoryRow>(query).ToList();
                ProgramCategoryList = result;
            }
            return ProgramCategoryList;

        }

        #region Prepare Letter Generated Template
        public LetterGeneratedTemplateRow PrepareLetterGeneratedTemplateForRugular(IRequestContext Context, IDbConnection connection, BillingInvoiceRow billing)
        {
            LetterGeneratedTemplateRow letterGT = new LetterGeneratedTemplateRow();

            var letterTemplDic = GetLetterTemplateByInvoiceTemplate(billing.InvoiceTemplate, Context, connection);
            string invoiceLetterTemp = letterTemplDic["Invoice"];
            string contactLetterTemp = letterTemplDic["Contract"];
            string transLetterTemp = letterTemplDic["Transmission"];

            var invoiceTemplate = ReplaceRegularInvoiceTemp(invoiceLetterTemp, billing, connection);
            var contractTemplate = ReplaceRegularInvoiceTemp(contactLetterTemp, billing, connection);
            var transmissionTemplate = ReplaceRegularInvoiceTemp(transLetterTemp, billing, connection);

            letterGT.BillingId = billing.Id;
            letterGT.InvoiceLetterTemplate = invoiceTemplate;
            letterGT.ContractLetterTemplate = contractTemplate;
            letterGT.TransmissionCertificateLetterTemplate = transmissionTemplate;

            return letterGT;
        }

        private string ReplaceRegularInvoiceTemp(string regulatInv, BillingInvoiceRow billing, IDbConnection connection)
        {
            #region Invoice
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString()));
            var TVChannelInfo = OrganizationInfoUtils.GetTVChannelInformation(connection);
            var fldClientTvc = ClientTvcRow.Fields.As("ClientTVC");
            var fldClient = ClientsRow.Fields.As("Clients");
            var fldRecClient = ClientsRow.Fields.As("RecClients");
            var fldRecAgency = AgenciesRow.Fields.As("RecAgencies");
            var fldAgency = AgenciesRow.Fields.As("Agencies");
            var fldClientContractRegular = ClientContractRegularRow.Fields.As("ClientContractRegular");
            var fldClientContract = ClientContractRow.Fields.As("ClientContract");

            int clientId = billing.ClientId != null ? billing.ClientId.Value : 0;
            int recipientClientId = billing.RecipientClientId != null ? billing.RecipientClientId.Value : 0;
            int recipientAgencyId = billing.RecipientAgencyId != null ? billing.RecipientAgencyId.Value : 0;
            int agencyId = billing.AgencyId != null ? billing.AgencyId.Value : 0;
            //int clientId = (int)billing.ClientId;

            var sqlQuery = new SqlQuery();
            if (recipientClientId != 0)
            {
                sqlQuery
                .From(fldClient)
                .LeftJoin(fldRecClient, fldRecClient.Id == recipientClientId)
                .LeftJoin(fldAgency, fldAgency.Id == agencyId)
                .LeftJoin(fldClientTvc, fldClientTvc.ClientId == clientId)
                .LeftJoin(fldClientContract, fldClientContract.ClientId == clientId)
                .InnerJoin(fldClientContractRegular, fldClientContractRegular.EffectiveFrom <= billing.FromDate.Value && fldClientContractRegular.EffectiveTo >= billing.ToDate.Value)
                .Where(fldClientContract.TvChannelId == tvChannelId && fldClient.Id == billing.ClientId.Value)
                .Select("RecClients.CompanyName AS ReceipientName, RecClients.BillAddress AS ReceipientAddress,\r\nClientContractRegular.RegularContractNumber ContractNo,\r\n FORMAT(ClientContractRegular.ContractDate, 'dd-MMMM-yyyy') ContractDate,\r\nClients.CompanyName Company,\r\nAgencies.CompanyName Advisor,\r\nClientTVC.ProductName Brand,\r\nClientTVC.TVCName TVCOrProduct,\r\nClientTVC.Duration Duration");

            }
            else if (recipientAgencyId != 0)
            {
                sqlQuery
               .From(fldClient)
               .LeftJoin(fldRecAgency, fldRecAgency.Id == recipientAgencyId)
               .LeftJoin(fldAgency, fldAgency.Id == agencyId)
               .LeftJoin(fldClientTvc, fldClientTvc.ClientId == clientId)
               .LeftJoin(fldClientContract, fldClientContract.ClientId == clientId)
               .InnerJoin(fldClientContractRegular, fldClientContractRegular.EffectiveFrom <= billing.FromDate.Value && fldClientContractRegular.EffectiveTo >= billing.ToDate.Value)
               .Where(fldClientContract.TvChannelId == tvChannelId && fldClient.Id == billing.ClientId.Value)
               .Select("RecAgencies.CompanyName AS ReceipientName, RecAgencies.BillAddress AS ReceipientAddress,\r\nClientContractRegular.RegularContractNumber ContractNo,\r\n FORMAT(ClientContractRegular.ContractDate, 'dd-MMMM-yyyy') ContractDate,\r\nClients.CompanyName Company,\r\nAgencies.CompanyName Advisor,\r\nClientTVC.ProductName Brand,\r\nClientTVC.TVCName TVCOrProduct,\r\nClientTVC.Duration Duration");


            }

            RegularLetterViewModel regularLetterVM = new RegularLetterViewModel();
            if (recipientClientId != 0 || recipientAgencyId != 0)
            {
                regularLetterVM = connection.Query<RegularLetterViewModel>(sqlQuery).FirstOrDefault();
            }
            regularLetterVM.Logo = TVChannelInfo.Logo;
            regularLetterVM.Address = TVChannelInfo.Address;
            regularLetterVM.Email = TVChannelInfo.Email;
            regularLetterVM.phone = TVChannelInfo.Phone;
            regularLetterVM.TVChannelName = TVChannelInfo.Name;
            regularLetterVM.IssueDate = billing.IssueDate.ToLongDateFormat();
            regularLetterVM.StartDate = billing.FromDate.ToLongDateFormat();
            regularLetterVM.EndDate = billing.ToDate.ToLongDateFormat();
            regularLetterVM.ContractOfTransmission = billing.FromDate.ToLongDateFormat() + " to " + billing.ToDate.ToLongDateFormat();
            regularLetterVM.Schedule = billing.Schedule;
            regularLetterVM.InvoiceNumber = billing.InvoiceNumber;
            regularLetterVM.PRFNo = billing.PrfNumber;
            regularLetterVM.InvoiceTemplate = billing.InvoiceTemplate.ToString();            
            decimal? NetMinute = 0;
            decimal? NetAmount = 0;
            int? NetDay = 0;
            foreach (var Invoice in billing.BillingInvoiceDetailList)
            {
                regularLetterVM.BillingDetailList.Add(new BillingDetail
                {
                    Position = Invoice.Position,
                    NetRate = Invoice.NetRate.ToString(),
                    BDT = Invoice.Bdt.ToString(),
                    Day = Invoice.Day.ToString(),
                    Min = Q.Round(Invoice.Min,2).ToString()
                });
                regularLetterVM.TVCType = Invoice.Position;
                NetMinute += Invoice.Min;
                NetAmount += Invoice.Bdt;
                NetDay += Invoice.Day;
            }
            regularLetterVM.MinNetTotal = Q.Round(NetMinute,2).ToString();
            regularLetterVM.BDTNetTotal = NetAmount.ToString();
            if (billing.InvoiceTemplate == Common.InvoiceTemplateType.Regular || billing.InvoiceTemplate == Common.InvoiceTemplateType.Doggy)
            {
                if (billing.BillingInvoiceDetailOtherAdditionList.Count > 0)
                {
                    regularLetterVM.VAT = billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount.ToString();
                    regularLetterVM.GrandTotal = (Q.Round(NetAmount + billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount)).ToString();
                    var GrandTotal = NetAmount + billing.BillingInvoiceDetailOtherAdditionList.Where(x => x.Particulars.Contains("VAT")).FirstOrDefault().Amount;
                    regularLetterVM.InWords = BmsUtility.NumberToWords(Convert.ToInt32(Q.Round(GrandTotal))) + " Taka Only";
                }
                else
                {
                    regularLetterVM.GrandTotal = Q.Round(NetAmount).ToString();
                }
            }
            else if (billing.InvoiceTemplate == Common.InvoiceTemplateType.Scrolling)
            {
                regularLetterVM.GrandTotal = Q.Round(NetAmount).ToString();
                regularLetterVM.InWords = BmsUtility.NumberToWords(Convert.ToInt32(Q.Round(NetAmount))) + " Taka Only";
            }

            #endregion

            return regulatInv.RegularInvoice(regularLetterVM);
        }

        public Dictionary<string, string> GetLetterTemplateByInvoiceTemplate(Common.InvoiceTemplateType templateType, IRequestContext Context, IDbConnection connection)
        {
            LetterTemplateRow lTemplate = new LetterTemplateRow();
            switch (templateType)
            {
                case Common.InvoiceTemplateType.Regular:
                    lTemplate = GetLetterTemplate(Common.InvoiceTemplateType.Regular, Context, connection);
                    break;
                case Common.InvoiceTemplateType.Scrolling:
                    lTemplate = GetScrollingLetterTemplate(Common.InvoiceTemplateType.Scrolling, Context, connection);
                    break;
                case Common.InvoiceTemplateType.Doggy:
                    lTemplate = GetDoggyLetterTemplate(Common.InvoiceTemplateType.Doggy, Context, connection);
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

            return new Setup.LetterTemplateListHandler(Context).List(connection, new ListRequest()).Entities.Where(x => (int)x.TvcType == 1).SingleOrDefault(); //TODO: change later
        }
        public LetterTemplateRow GetScrollingLetterTemplate(Common.InvoiceTemplateType template, IRequestContext Context, IDbConnection connection)
        {

            return new Setup.LetterTemplateListHandler(Context).List(connection, new ListRequest()).Entities.Where(x => (int)x.TvcType == 4).SingleOrDefault(); //TODO: change later
        }
        public LetterTemplateRow GetDoggyLetterTemplate(Common.InvoiceTemplateType template, IRequestContext Context, IDbConnection connection)
        {

            return new Setup.LetterTemplateListHandler(Context).List(connection, new ListRequest()).Entities.Where(x => (int)x.TvcType == 5).SingleOrDefault(); //TODO: change later
        }
        #endregion
    }
    public class RegularViewModel
    {
        public List<BillingInvoiceDetailAsPerAiredRow> AsPerAireds { get; set; }
        public List<BillingInvoiceDetailAsPerAiredRow> GPRPAsPerAireds { get; set; }
        public List<BillingInvoiceDetailAsPerAiredRow> ScrollAsPerAireds { get; set; }
        public List<BillingInvoiceDetailOtherAdditionRow> OtherAdditions { get; set; }
        public List<BillingInvoiceDetailOtherDeductionRow> OtherDeductions { get; set; }
        public List<BillingInvoiceDetailRow> InvoiceDetails { get; set; }
    }

    public static class StringExtention
    {
        public static string RegularInvoice(this string s, RegularLetterViewModel model)
        {

            #region Generate Detail Table
            string BillingDetailTable = "";
            string InitTable = "";
            string BillingDetailTransmission = "";
            string InitTransmission = "";
            if (model.InvoiceTemplate == Common.InvoiceTemplateType.Regular.ToString())
            {
                InitTable = "<table style = 'padding-left:5px;font-size:14px;'><thead style='font-weight:bold;background-color:lightgray;'><tr><td style='width: 250px'>Position</td><td style='width: 150px'>Net Rate</td><td style='width: 150px'>Min.</td><td style='width: 100px;text-align: right'>BDT</td></tr></thead><tbody>";
                InitTransmission = "<table style = 'padding-left:5px;font-size:14px;'><thead style='font-weight:bold;background-color:lightgray;'><tr><td style='width: 250px'>Details</td><td style='width: 200px'></td><td style='width: 200px'>Min.</td></tr></thead><tbody>";
                foreach (var item in model.BillingDetailList)
                {                
                    BillingDetailTable += $"<tr><td>{item.Position}</td><td>{item.NetRate}</td><td>{item.Min}</td><td style='text-align: right;'>{item.BDT}</td></tr>";
                    BillingDetailTransmission += $"<tr><td>{item.Position}</td><td> - </td><td>{item.Min}</td></tr>";

                }

            }
            else if (model.InvoiceTemplate == Common.InvoiceTemplateType.Scrolling.ToString() || model.InvoiceTemplate == Common.InvoiceTemplateType.Doggy.ToString())
            {
                InitTable = "<table style = 'padding-left:5px;font-size:14px;'><thead style='font-weight:bold;background-color:lightgray;'><tr><td style='width: 150px'>Position</td><td style='width: 150px'>Rate</td><td style='width: 150px'>Day</td><td style='width: 100px;text-align: right'>BDT</td></tr></thead><tbody>";
                foreach (var item in model.BillingDetailList)
                {
                    BillingDetailTable += $"<tr><td>{item.Position}</td><td>{item.NetRate}</td><td>{item.Day}</td><td style='text-align: right;'>{item.BDT}</td></tr>";
                }
            }
          
            string BillingTableDesign = InitTable + BillingDetailTable + "</tbody></table>";
            model.BillingDetail = BillingTableDesign;
            string TransmissionTableDesign = InitTransmission + BillingDetailTransmission + "</tbody></table>";
            model.BillingTransmissionDetail = TransmissionTableDesign;
            #endregion

            StringBuilder sb = new StringBuilder(s);

            sb.Replace("[[Receipient Name]]", model.ReceipientName);
            sb.Replace("[[Receipient Address]]", model.ReceipientAddress);
            sb.Replace("[[Contract No.]]", model.ContractNo);
            sb.Replace("[[Issue Date]]", model.IssueDate);
            sb.Replace("[[Contract Date]]", model.ContractDate);
            sb.Replace("[[Start Date]]", model.StartDate);
            sb.Replace("[[End Date]]", model.EndDate);
            sb.Replace("[[Invoice No.]]", model.InvoiceNumber);
            sb.Replace("[[Contract of Transmission]]", model.ContractOfTransmission);
            sb.Replace("[[Company]]", model.Company);
            sb.Replace("[[Advertiser]]", model.Advisor);
            sb.Replace("[[TVC/Product]]", model.TVCOrProduct);
            sb.Replace("[[TVC Duration]]", model.Duration);
            sb.Replace("[[Schedule]]", model.Schedule);
            sb.Replace("[[Billing Invoice Detail]]", model.BillingDetail);            
            sb.Replace("[[Billing Invoice Transmission Detail]]", model.BillingTransmissionDetail);            
            sb.Replace("[[Min Net Total]]", model.MinNetTotal);
            sb.Replace("[[BDT Net Total]]", model.BDTNetTotal);
            sb.Replace("[[Add(%)VAT]]", model.VAT);
            sb.Replace("[[Grand Total]]", model.GrandTotal);
            sb.Replace("[[Grand Total In word]]", model.InWords);
            sb.Replace("[[PRF No.]]", model.PRFNo);
            sb.Replace("[[Position]]", model.TVCType);
            
            //Report Header
            sb.Replace("[[TV Channel Logo]]", model.Logo);
            sb.Replace("[[TVChannel Address]]", model.Address);
            sb.Replace("[[Email]]", model.Email);
            sb.Replace("[[Contact]]", model.phone);
            sb.Replace("[[TVChannel Name]]", model.TVChannelName);


            return sb.ToString();
        }
    }

    public class RegularLetterViewModel
    {
        public string ReceipientName { get; set; }
        public string ReceipientAddress { get; set; }
        public string ContractNo { get; set; }
        public string ContractDate { get; set; }
        public string IssueDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ContractOfTransmission { get; set; }
        public string Company { get; set; }
        public string Advisor { get; set; }
        public string InvoiceNumber { get; set; }
        public string Brand { get; set; }
        public string TVCOrProduct { get; set; }
        public string Duration { get; set; }
        public string Schedule { get; set; }
        public List<BillingDetail> BillingDetailList { get; set; } = new List<BillingDetail>();
        public string MinNetTotal { get; set; }
        public string BDTNetTotal { get; set; }
        public string VAT { get; set; }
        public string GrandTotal { get; set; }
        public string InWords { get; set; }
        public string PRFNo { get; set; }
        public string BillingDetail { get; set; }
        public string BillingTransmissionDetail { get; set; }
        public string InvoiceTemplate { get; set; }
        public string TVCType { get; set; }


        //Report Header
        public string Logo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string TVChannelName { get; set; }
    }

    public class BillingDetail
    {
        public string Position { get; set; }
        public string NetRate { get; set; }
        public string Min { get; set; }
        public string BDT { get; set; }
        public string Day { get; set; }
    }


}

