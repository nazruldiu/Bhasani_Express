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
    public class BMSInvoiceDoggy
    {
        protected ISqlConnections SqlConnections;

        public BMSInvoiceDoggy()
        {
            this.SqlConnections = BMS_Scheduler.AppStatics.SqlConnections;
        }
        public RegularViewModel ProcessDoggyInvoice(IRequestContext Context, IDbConnection connection, GetInvoiceRequest getInvoiceRequest)
        {
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString(), Context));
            var dailyRundownList = new BmsUtility().GetDailyRundown(tvChannelId, Context, getInvoiceRequest);
            var DoggyList = GetDoggyList(tvChannelId, Context, getInvoiceRequest);

            #region Inprogress:: Calculation and Set Billing Invoice Detail And As Per Aired
            List<BillingInvoiceDetailRow> billingInvoiceDetailList = new List<BillingInvoiceDetailRow>();

            List<BillingInvoiceDetailAsPerAiredRow> billingInvoiceDetailAsPerAiredList = new List<BillingInvoiceDetailAsPerAiredRow>();

            foreach (var doggy in DoggyList)
            {
                var list = new Setup.RateBasedOnListHandler(Context).List(connection, new ListRequest()).Entities;
                int? monthId = Convert.ToInt32(list.Where(x => x.Name.Contains("Month")).FirstOrDefault().Id);
                int dayId = Convert.ToInt32(list.Where(x => x.Name.Contains("Day")).FirstOrDefault().Id);
                if (doggy.RateBasedOn == monthId)
                {
                    BillingInvoiceDetailRow billInvoiceDetail = new BillingInvoiceDetailRow();
                    BillingInvoiceDetailAsPerAiredRow DoggyAsPerAired = new BillingInvoiceDetailAsPerAiredRow();

                    var DoggyBroadCastList = dailyRundownList.Where(rundown => rundown.RundownDate >= doggy.EffectiveFrom && rundown.RundownDate <= doggy.EffectiveTo && rundown.TvcTypeId == doggy.TvcTypeId && rundown.TvcId == doggy.ClientTvcId).FirstOrDefault();

                    billInvoiceDetail.Position = doggy.Position;
                    billInvoiceDetail.NetRate = doggy.Rate;
                    billInvoiceDetail.Day = 0;
                    billInvoiceDetail.Bdt = doggy.Rate;

                    DoggyAsPerAired.Particulars = doggy.Position;
                    DoggyAsPerAired.NetRate = doggy.Rate;
                    DoggyAsPerAired.Day = 0;
                    DoggyAsPerAired.Amount = doggy.Rate;

                    billingInvoiceDetailList.Add(billInvoiceDetail);
                    billingInvoiceDetailAsPerAiredList.Add(DoggyAsPerAired);

                }

                else if(doggy.RateBasedOn == dayId)
                {
                    var DoggyBroadCastList = dailyRundownList.Where(rundown => rundown.RundownDate >= doggy.EffectiveFrom && rundown.RundownDate <= doggy.EffectiveTo && rundown.TvcTypeId == doggy.TvcTypeId && rundown.TvcId == doggy.ClientTvcId).ToList();
                   var asPerAired = DoggyBroadCastList.GroupBy(q => q.TvcId).Select(s => new BillingInvoiceDetailAsPerAiredRow
                    {
                        Particulars = doggy.Position,
                        NetRate = doggy.Rate,
                        Day = s.Count(),
                        Amount = (doggy.Rate * s.Count())
                    }).ToList();

                   var billingInvoiceDetail = DoggyBroadCastList.GroupBy(q => q.TvcId).Select(s => new BillingInvoiceDetailRow
                    {
                        Position = doggy.Position,
                        NetRate = doggy.Rate,
                        Day = s.Count(),
                        Bdt = (doggy.Rate * s.Count())
                    }).ToList();

                    #region InProgress:: Set As Per Aired                                    

                    billingInvoiceDetailAsPerAiredList.AddRange(asPerAired);
                    #endregion

                    #region Inprogress:: Set Billing Invoice Detail

                    billingInvoiceDetailList.AddRange(billingInvoiceDetail);
                    #endregion
                }


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
       
        public BillingInvoiceRow ConvertDoggyViewModelToBillingInvoice(RegularViewModel model, GetInvoiceRequest invoice)
        {
            invoice.BillingInvoice.BillingInvoiceDetailList = model.InvoiceDetails;
            invoice.BillingInvoice.ScrollingBillingInvoiceDetailAsPerAiredList = model.AsPerAireds;
            invoice.BillingInvoice.BillingInvoiceDetailOtherAdditionList = model.OtherAdditions;
            invoice.BillingInvoice.BillingInvoiceDetailOtherDeductionList = model.OtherDeductions;

            return invoice.BillingInvoice;
        }

        public List<ClientContractDoggyRow> GetDoggyList(long tvChannelId, IRequestContext Context, GetInvoiceRequest req)
        {
            var fldClientContractDoggy= ClientContractDoggyRow.Fields.As("fldClientContractDoggy");
            var fldClientContractDoggyDetail = ClientContractDoggyDetailRow.Fields.As("fldClientContractDoggyDetail");
            var fldTvcType = TVCTypeRow.Fields.As("fldTvcType");
            var fldRateBasedOn = RateBasedOnRow.Fields.As("fldRateBasedOn");

            var query = new SqlQuery()
                .Select(fldTvcType.Name, "Position")
                .Select(fldClientContractDoggyDetail.TvcTypeId)
                .Select(fldClientContractDoggyDetail.ClientTvcId)
                .Select(fldClientContractDoggy.Rate)
                .Select(fldRateBasedOn.Name, "RateBasedOnString")
                .Select(fldClientContractDoggy.RateBasedOn)
                .Select(fldClientContractDoggy.EffectiveFrom)
                .Select(fldClientContractDoggy.EffectiveTo)
                .From(fldClientContractDoggy)
                .InnerJoin(fldClientContractDoggyDetail, fldClientContractDoggyDetail.ClientContractDoggyId == fldClientContractDoggy.Id)
                .InnerJoin(fldTvcType, fldClientContractDoggyDetail.TvcTypeId == fldTvcType.Id)
                .InnerJoin(fldRateBasedOn, fldClientContractDoggy.RateBasedOn == fldRateBasedOn.Id);


            List<ClientContractDoggyRow> DoggyDetailList = new List<ClientContractDoggyRow>();
            using (var connection = SqlConnections.NewFor<ClientContractDoggyRow>())
            {
                var result = connection.Query<ClientContractDoggyRow>(query).ToList();
                DoggyDetailList = result;
            }

            return DoggyDetailList;
        }

    }
}
