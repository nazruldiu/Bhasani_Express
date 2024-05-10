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
    public class BMSInvoiceScrolling
    {
        protected ISqlConnections SqlConnections;

        public BMSInvoiceScrolling()
        {
            this.SqlConnections = BMS_Scheduler.AppStatics.SqlConnections;
        }
        public RegularViewModel ProcessScrollingInvoice(IRequestContext Context, IDbConnection connection, GetInvoiceRequest getInvoiceRequest)
        {
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString(), Context));
            var dailyRundownList = new BmsUtility().GetDailyRundown(tvChannelId, Context, getInvoiceRequest);
            var ScrollingList = GetScrollingList(tvChannelId, Context, getInvoiceRequest);

            #region Inprogress:: Calculation and Set Billing Invoice Detail And As Per Aired
            List<BillingInvoiceDetailRow> billingInvoiceDetailList = new List<BillingInvoiceDetailRow>();

            List<BillingInvoiceDetailAsPerAiredRow> billingInvoiceDetailAsPerAiredList = new List<BillingInvoiceDetailAsPerAiredRow>();

            foreach (var scrolling in ScrollingList)
            {
                var scrollingBroadCastList = dailyRundownList.Where(rundown => rundown.RundownDate >= scrolling.EffectiveFrom && rundown.RundownDate <= scrolling.EffectiveTo && rundown.TvcTypeId == scrolling.TvcTypeId && rundown.TvcId == scrolling.ClientTvcId).ToList();
                var asPerAired = scrollingBroadCastList.GroupBy(q => q.TvcId).Select(s => new BillingInvoiceDetailAsPerAiredRow
                {
                    Particulars = scrolling.Position,
                    NetRate = scrolling.Rate,
                    Day = s.Count(),
                    Amount = (scrolling.Rate * s.Count())
                }).ToList();

                var billingInvoiceDetail = scrollingBroadCastList.GroupBy(q => q.TvcId).Select(s => new BillingInvoiceDetailRow
                {
                    Position = scrolling.Position,
                    NetRate = scrolling.Rate,
                    Day = s.Count(),
                    Bdt = (scrolling.Rate * s.Count())
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
                OtherAdditions = billingInvoiceDetailOtherAdditionList,
                OtherDeductions = billingInvoiceDetailOtherDeductionList
            };
        }

        public BillingInvoiceRow ConvertScrollingViewModelToBillingInvoice(RegularViewModel model, GetInvoiceRequest invoice)
        {
            invoice.BillingInvoice.BillingInvoiceDetailList = model.InvoiceDetails;
            invoice.BillingInvoice.ScrollingBillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            invoice.BillingInvoice.BillingInvoiceDetailOtherAdditionList = model.OtherAdditions;
            invoice.BillingInvoice.BillingInvoiceDetailOtherDeductionList = model.OtherDeductions;

            return invoice.BillingInvoice;
        }

        public List<ClientContractScrollingRow> GetScrollingList(long tvChannelId, IRequestContext Context, GetInvoiceRequest req)
        {
            var fldClientContractScrolling = ClientContractScrollingRow.Fields.As("fldClientContractScrolling");
            var fldClientContractScrollingDetail = ClientContractScrollingDetailRow.Fields.As("fldClientContractScrollingDetail");
            var fldTvcType = TVCTypeRow.Fields.As("fldTvcType");
            var fldRateBasedOn = RateBasedOnRow.Fields.As("fldRateBasedOn");

            var query = new SqlQuery()
                .Select(fldTvcType.Name, "Position")
                .Select(fldClientContractScrollingDetail.TvcTypeId)
                .Select(fldClientContractScrollingDetail.ClientTvcId)
                .Select(fldClientContractScrolling.Rate)
                .Select(fldRateBasedOn.Name, "RateBasedOnString")
                .Select(fldClientContractScrolling.EffectiveFrom)
                .Select(fldClientContractScrolling.EffectiveTo)
                .From(fldClientContractScrolling)
                .InnerJoin(fldClientContractScrollingDetail, fldClientContractScrollingDetail.ClientContractScrollingId == fldClientContractScrolling.Id)
                .InnerJoin(fldTvcType, fldClientContractScrollingDetail.TvcTypeId == fldTvcType.Id)
                .InnerJoin(fldRateBasedOn, fldClientContractScrolling.RateBasedOn == fldRateBasedOn.Id);


            List<ClientContractScrollingRow> ScrollingDetailList = new List<ClientContractScrollingRow>();
            using (var connection = SqlConnections.NewFor<ClientContractScrollingRow>())
            {
                var result = connection.Query<ClientContractScrollingRow>(query).ToList();
                ScrollingDetailList = result;
            }

            return ScrollingDetailList;
        }
    }
}
