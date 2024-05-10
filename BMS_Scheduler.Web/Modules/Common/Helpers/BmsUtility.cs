using BMS_Scheduler.Billing;
using BMS_Scheduler.Billing.Endpoints;
using BMS_Scheduler.Common.Helpers;
using BMS_Scheduler.Setup;
using BMS_Scheduler.Setup.Endpoints;
using BMS_Scheduler.Task;
using BMS_Scheduler.Tax;
using BMS_Scheduler.Web.Modules.Common;
using BMS_Scheduler.Web.Modules.Common.Business.Services;
using BMS_Scheduler.Web.Modules.Common.Helpers;
using ExtensionMethods;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NUglify.Helpers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
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
    public class BmsUtility
    {
        protected ISqlConnections SqlConnections;

        public BmsUtility()
        {
            this.SqlConnections = BMS_Scheduler.AppStatics.SqlConnections;
        }
        public void GetTime(int totalSecond, out int calculatedHour, out int calculatedMinute, out int calculatdSecond)
        {
            var inHour = Convert.ToDecimal(totalSecond / 3600.0);
            var hourPart = Math.Truncate(inHour);
            var remainBalance = inHour - hourPart;
            var minute = remainBalance * 60;
            var minutePart = Math.Truncate(minute);
            remainBalance = minute - minutePart;
            var seconds = (remainBalance * 60);
            var secondPart = 0;
            if (seconds > 59)
            {
                minutePart = minutePart + 1;
                secondPart = 0;
            }
            else
            {
                secondPart = (int)Math.Round(remainBalance * 60);
            }

            calculatedHour = Convert.ToInt32(hourPart);
            calculatedMinute = Convert.ToInt32(minutePart);
            calculatdSecond = Convert.ToInt32(secondPart);
        }
        public string GetTime(int? second, bool showSecond = false)
        {
            var inHour = Convert.ToDecimal(second / 3600.0);
            var hourPart = Math.Truncate(inHour);
            var remainBalance = inHour - hourPart;
            var minute = remainBalance * 60;
            var minutePart = Math.Truncate(minute);
            remainBalance = minute - minutePart;
            var seconds = (remainBalance * 60);
            var secondPart = 0;
            if (seconds > 59)
            {
                minutePart = minutePart + 1;
                secondPart = 0;
            }
            else
            {
                secondPart = (int)Math.Round(remainBalance * 60);
            }
            if (!showSecond)
                return hourPart.ToString().PadLeft(2, '0') + ":" + minutePart.ToString().PadLeft(2, '0');
            else
                return hourPart.ToString().PadLeft(2, '0') + ":" + minutePart.ToString().PadLeft(2, '0') + ":" + secondPart.ToString().PadLeft(2, '0');
            ;
        }
        public string GetTimeSlotNameByTime(int sec, int tvChannelId)
        {
            string timeSlotName;

            var query = new SqlQuery()
                        .From("TimeSlot")
                        .Select("Name")
                        .Where(
                            new Criteria("StartTime") <= sec &
                            new Criteria("EndTime") >= sec &
                            new Criteria("TVChannelId") == tvChannelId);

            using (var connection = SqlConnections.NewFor<DailyRundownRow>())
            {
                timeSlotName = connection.Query<string>(query).SingleOrDefault();
            }

            return timeSlotName;
        }
        public void GetOnAirTimeSetup(IRequestContext Context, DateTime date, out OnAirTimeSetupRow onAirTime)
        {
            int tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString()));

            var fld = OnAirTimeSetupRow.Fields.As("OnAirTime");

            var sqlQuery = new SqlQuery()
                .From(fld)
                .Where(fld.TvChannelId == tvChannelId && fld.EffectiveDate <= date)
                .Select(fld.Id, fld.StartTime, fld.EndTime, fld.EffectiveDate, fld.StartingBuffer, fld.EndingBuffer)
                .OrderBy(fld.EffectiveDate, desc: true)
                .Take(1);

            using (var connection = SqlConnections.NewFor<DailyRundownRow>())
            {
                onAirTime = connection.Query<OnAirTimeSetupRow>(sqlQuery).FirstOrDefault();
                GetDateFromStartTimeEndTime(onAirTime, onAirTime.StartTime.Value, onAirTime.EndTime.Value, date, out DateTime segmentStartDate, out DateTime segmentEndTime);
                onAirTime.SegmentStartTime = segmentStartDate;
                onAirTime.SegmentEndTime = segmentEndTime;
            }


            //onAirTime = new OnAirTimeSetupRow();
            //string query = $@"
            //        Select
            //         T0.Id
            //         ,T0.StartTime
            //         ,T0.EndTime
            //         ,T0.EffectiveDate
            //        From OnAirTimeSetup T0
            //        Inner Join
            //        (
            //        Select
            //         Max(T0.EffectiveDate) EffectiveDate
            //        From OnAirTimeSetup T0
            //        Where T0.EffectiveDate <= @EffectiveDate
            //        And T0.TVChannelId = @TvChannelId
            //        )LastValue On T0.EffectiveDate = LastValue.EffectiveDate";
            //var param = new Dapper.DynamicParameters();
            //param.Add("@EffectiveDate", date, dbType: DbType.Date, direction: ParameterDirection.Input);
            //param.Add("@TvChannelId", tvChannelId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            //using (var connection = SqlConnections.NewFor<DailyRundownRow>())
            //{
            //    var result = connection.Query<OnAirTimeSetupRow>(sql: query, param: param, commandType: CommandType.Text, commandTimeout: 0).FirstOrDefault();
            //    onAirTime = result;
            //}
        }
        public void GetDateFromStartTimeEndTime(OnAirTimeSetupRow onAirTime, int startTime, int endTime, DateTime date, out DateTime segmentStartDateTime, out DateTime segmentEndDateTime)
        {
            DateTime nextDate = date.AddDays(1);

            if (startTime >= 1 && startTime < onAirTime.StartTime.Value) // Next Date
            {
                new BmsUtility().GetTime(startTime, out int hour, out int minute, out int second);
                segmentStartDateTime = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, hour, minute, second);
            }
            else // Same Date
            {
                new BmsUtility().GetTime(startTime, out int hour, out int minute, out int second);
                segmentStartDateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
            }

            if (endTime >= 1 && endTime < onAirTime.StartTime.Value) // Next Date
            {
                new BmsUtility().GetTime(endTime, out int hour, out int minute, out int second);
                segmentEndDateTime = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day, hour, minute, second);
            }
            else // Same Date
            {
                new BmsUtility().GetTime(endTime, out int hour, out int minute, out int second);
                segmentEndDateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
            }

        }
        public int GetTotalSecond(DateTime datetime)
        {
            return (datetime.Hour * 3600) + (datetime.Minute * 60) + datetime.Second;
        }
        private static string GetImagePath(string path)
        {
            return Path.Combine(AppStatics.UploadSettings.Path.Replace("~/", ""), path);
        }
        public List<GPRPTimeListModel> SequentialTimeRangeByInterval(IRequestContext context, IDbConnection connection, SequentialTimeRangeByIntervalRequest request)
        {
            GetOnAirTimeSetup(context, request.EffectiveFrom, out OnAirTimeSetupRow onAirTime);
            GetDateFromStartTimeEndTime(onAirTime, request.StartTime, request.EndTime, request.EffectiveFrom, out DateTime segmentStartTime, out DateTime segmentEndTime);

            List<GPRPTimeListModel> GPRPTimeListModelList = new List<GPRPTimeListModel>();
            for (DateTime i = segmentStartTime; i < segmentEndTime.AddMilliseconds(-segmentStartTime.Millisecond); segmentStartTime.AddMilliseconds(-segmentStartTime.Millisecond).AddMinutes(request.IntervalMin))
            {
                GPRPTimeListModel gPRPTimeListModel = new GPRPTimeListModel();

                gPRPTimeListModel.startDateTime = segmentStartTime;
                gPRPTimeListModel.endDateTime = segmentStartTime.AddMinutes(request.IntervalMin).AddSeconds(-1);
                gPRPTimeListModel.startTimeEndTimeString = GetTime((int)segmentStartTime.AddMilliseconds(-segmentStartTime.Millisecond).TimeOfDay.TotalSeconds, true) + "-" + GetTime((int)segmentStartTime.AddMinutes(request.IntervalMin).AddSeconds(-1).AddMilliseconds(-segmentStartTime.Millisecond).TimeOfDay.TotalSeconds, true);
                gPRPTimeListModel.startTime = (int)segmentStartTime.AddMilliseconds(-segmentStartTime.Millisecond).TimeOfDay.TotalSeconds;
                gPRPTimeListModel.endTime = (int)segmentStartTime.AddMilliseconds(-segmentStartTime.Millisecond).AddMinutes(request.IntervalMin).AddSeconds(-1).TimeOfDay.TotalSeconds;

                GPRPTimeListModelList.Add(gPRPTimeListModel);

                segmentStartTime = gPRPTimeListModel.endDateTime.AddSeconds(1);

                if (segmentStartTime >= segmentEndTime)
                    break;
            }

            return GPRPTimeListModelList;
        }

        #region Billing Invoice

        public List<DailyRundownRow> GetDailyRundown(long tvChannelId, IRequestContext Context, GetInvoiceRequest req)
        {

            var fldClientTvc = ClientTvcRow.Fields.As("ClientTVC");
            var fldDailyRundown = DailyRundownRow.Fields.As("DailyRundown");
            var fldTimeSlot = TimeSlotRow.Fields.As("TimeSlot");
            var fldDailyRundownTimeSegment = DailyRundownTimeSegmentPositionRow.Fields.As("DailyRundownTimeSegment");
            var fldDailyRundownTimeSegmentDetail = DailyRundownTimeSegmentDetailsRow.Fields.As("DailyRundownTimeSegmentDetail");

            var sqlQuery = new SqlQuery()
                .From(fldDailyRundown)
                .InnerJoin(fldClientTvc, fldClientTvc.Id == fldDailyRundown.TvcId)
                .InnerJoin(fldDailyRundownTimeSegment, fldDailyRundown.SegmentPositionId == fldDailyRundownTimeSegment.Id)
                .InnerJoin(fldDailyRundownTimeSegmentDetail, fldDailyRundownTimeSegment.RundownTimeSegmentDetailsId == fldDailyRundownTimeSegmentDetail.Id)
                .LeftJoin(fldTimeSlot, fldDailyRundown.StartTime >= fldTimeSlot.StartTime && fldDailyRundown.StartTime <= fldTimeSlot.EndTime)
                .Where(fldTimeSlot.TvChannelId == tvChannelId && fldDailyRundown.RundownDate >= req.FromDate.ToLongDateFormat()
                && fldDailyRundown.RundownDate <= req.ToDate.ToLongDateFormat()
                && fldClientTvc.ClientId == req.ClientId);
            if (req.TVCList.Any())
            {
                sqlQuery.Where(fldDailyRundown.TvcId.In(req.TVCList));
            }
            sqlQuery.Select("DailyRundown.*, TimeSlot.Name TimeSlotName,ClientTVC.BillableDuration NotMappedTVCDuration, ClientTVC.BillableFrame NotMappedTVCFrame,DailyRundownTimeSegmentDetail.ProgramCategoryId FPCProgramCategoryId,DailyRundown.StartTime,DailyRundown.EndTime,DailyRundownTimeSegment.CommercialPositionId AS SegmentPositionCommercialPositionId");
            List<DailyRundownRow> rundownRows = new List<DailyRundownRow>();
            using (var connection = SqlConnections.NewFor<DailyRundownRow>())
            {
                var result = connection.Query<DailyRundownRow>(sqlQuery).ToList();
                rundownRows = result;
            }

            return rundownRows;
        }
        
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        #endregion

        #region Get BMS TVChannel Logo Path
        public static ImageResult GetImage64(String imageName)
        {
            ImageResult img = new ImageResult();

            if (!String.IsNullOrEmpty(imageName))
            {
                BmsUtility foo = new BmsUtility();
                var imagePath = GetImagePath(imageName);
                if (!String.IsNullOrEmpty(imagePath))
                {
                    using (var webClient = new System.Net.WebClient())
                    {
                        try
                        {
                            img.Image64Byte = webClient.DownloadData(imagePath);

                            string base64String = Convert.ToBase64String(img.Image64Byte, 0, img.Image64Byte.Length);
                            img.ImageBase64 = "data:image/png;base64," + base64String;
                        }
                        catch (Exception) { }
                    }
                }
            }

            return img;
        }
        #endregion
    }

    #region Get BMS Report Header

    public static class OrganizationInfoUtils
    {
        public static OrganizationInfoViewModel _organizationInfoModel { get; set; }
        public static DataTable GetOrganizationInfornation()
        {
            using (var connection = AppStatics.SqlConnections.NewFor<UserPreferenceRow>())
            {
                var data = GetTVChannelInformation(connection);
                _organizationInfoModel = data;

                var obj = new ObjToDT();
                return obj.objToDataTable(_organizationInfoModel);
            }
        }
        public static OrganizationInfoViewModel GetTVChannelInformation(IDbConnection connection)
        {
            var param = new Dapper.DynamicParameters();
            var tvChannelInfofld = Setup.TVChannelBasicInfoRow.Fields.As("tvChannelInfofld");
            var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(Common.IdentityClaimType.TvChannelId.ToDescriptionString()));
            var query = new SqlQuery();
            query
                .Select(tvChannelInfofld.Name)
                .Select(tvChannelInfofld.Address)
                .Select(tvChannelInfofld.Phone)
                .Select(tvChannelInfofld.Email)
                .Select(tvChannelInfofld.Fax)
                .Select(tvChannelInfofld.Logo)
                .From(tvChannelInfofld)
                .Where(tvChannelInfofld.Id == tvChannelId)
                .OrderBy(tvChannelInfofld.IDate, desc: true);
            var data = connection.Query<OrganizationInfoViewModel>(query, commandTimeout: 0).FirstOrDefault();
            if (data != null && data.Logo != null)
            {
                var imgName = data.Logo;

                ImageResult img = BmsUtility.GetImage64(imgName);
                if (img != null)
                {
                    data.Image64Byte = img.Image64Byte;
                    data.Logo = img.ImageBase64;
                }
            }
            return data;
        }
    }

    public class ObjToDT
    {
        public DataTable objToDataTable(OrganizationInfoViewModel obj)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Address", typeof(String));
            dt.Columns.Add("Phone", typeof(String));
            dt.Columns.Add("Image64Byte", typeof(byte[]));

            DataRow dr = dt.NewRow();
            dr["Name"] = obj.Name;
            dr["Address"] = obj.Address;
            dr["Phone"] = obj.Phone;
            dr["Image64Byte"] = obj.Image64Byte;

            dt.Rows.Add(dr);

            return dt;
        }
    }

    public class ImageResult
    {
        public byte[] Image64Byte { get; set; }
        public String ImageBase64 { get; set; }
    }
    public class OrganizationInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public String Logo { get; set; }
        public byte[] Image64Byte { get; set; }
    }
    public class GPRPLetterViewModel
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
        public string GrossTotal { get; set; }
        public string VAT { get; set; }
        public string Total { get; set; }
        public string Less { get; set; }
        public string GrandTotal { get; set; }
        public string InWords { get; set; }
        public string RecName { get; set; }
        //Report Header
        public string Logo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string TVChannelName { get; set; }
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

namespace ExtensionMethods
{
    public static class BMSExtensions
    {
        public static int TimeInSecond(this string str)
        {
            int totalSecond = 0;
            if (str.Length > 8)
            {
                str = str.Substring(0, 8);
            }
            var hourMinute = str.Split(":");
            if (hourMinute.Length >= 0)
            {
                var hour = Convert.ToInt32(hourMinute[0]);
                var minute = Convert.ToInt32(hourMinute[1]);
                var second = 0;
                if (hourMinute.Length > 1)
                    second = Convert.ToInt32(hourMinute[2]);
                totalSecond = (hour * 60 * 60) + (minute * 60) + second;
            }

            return totalSecond;
        }
        public static bool IsBetweenTwoDates(this DateTime dt, DateTime start, DateTime end)
        {
            return dt >= start && dt <= end;
        }
        public static string GetEnumDisplayName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }
        public static bool IsPick(this string value)
        {
            if (value.Contains("Off"))
                return false;
            else
                return true;
        }
    }

    public static class StringExtention
    {
        public static string gprpTemplate(this string s, GPRPLetterViewModel model)
        {
            StringBuilder sb = new StringBuilder(s);

            sb.Replace("[[Receipient Name]]", model.RecName);
            sb.Replace("[[Receipient Address]]", model.Too);
            sb.Replace("[[Contract No.]]", model.ContractNo);
            sb.Replace("[[Issue Date]]", model.StartDate);
            sb.Replace("[[Contract Date]]", model.ContractDate);
            sb.Replace("[[Start Date]]", model.StartDate);
            sb.Replace("[[End Date]]", model.EndDate);
            sb.Replace("[[Invoice No.]]", model.ContractNo);
            sb.Replace("[[Contract of Transmission]]", model.ContractOfTransmission);
            sb.Replace("[[Company]]", model.Company);
            sb.Replace("[[Advertiser]]", model.Advisor);
            sb.Replace("[[TVC/Product]]", model.TVCOrProduct);
            sb.Replace("[[TVC Duration]]", model.Duration);
            sb.Replace("[[Net Rate]]", model.Total);
            sb.Replace("[[Grand Total]]", model.GrandTotal);
            sb.Replace("[[Grand Total In word]]", model.InWords);
            sb.Replace("[[Schedule]]", model.Schedule);

            sb.Replace("[[Commercial]]", model.Brand);
            sb.Replace("[[ACD]]", model.ACD);
            sb.Replace("[[GRPs]]", model.GRP);
            sb.Replace("[[BDT]]", model.BDT);
            sb.Replace("[[Gross Total]]", model.BDT);
            sb.Replace("[[VAT]]", model.VAT);
            sb.Replace("[[Total]]", model.Total);
            sb.Replace("[[Less]]", model.Less);
            sb.Replace("[[Brand]]", model.Brand);

            //Report Header
            sb.Replace("[[TV Channel Logo]]", model.Logo);
            sb.Replace("[[TVChannel Address]]", model.Address);
            sb.Replace("[[Email]]", model.Email);
            sb.Replace("[[Contact]]", model.phone);
            sb.Replace("[[TVChannel Name]]", model.TVChannelName);

            return sb.ToString();
        }

        public static string brandingTemplate(this string s, BrandingLetterViewModel model)
        {
            StringBuilder sb = new StringBuilder(s);

            sb.Replace("[[Receipient Name]]", model.RecName);
            sb.Replace("[[Receipient Address]]", model.Too);
            sb.Replace("[[Contract No.]]", model.ContractNo);
            sb.Replace("[[Issue Date]]", model.StartDate);
            sb.Replace("[[Contract Date]]", model.ContractDate);
            sb.Replace("[[Start Date]]", model.StartDate);
            sb.Replace("[[End Date]]", model.EndDate);
            sb.Replace("[[Invoice No.]]", model.ContractNo);
            sb.Replace("[[Contract of Transmission]]", model.ContractOfTransmission);
            sb.Replace("[[Company]]", model.Company);
            sb.Replace("[[Advertiser]]", model.Advisor);
            sb.Replace("[[TVC Duration]]", model.Duration);
            sb.Replace("[[Net Rate]]", model.Total);
            sb.Replace("[[Grand Total]]", model.GrandTotal);
            sb.Replace("[[Grand Total In word]]", model.InWords);
            sb.Replace("[[Schedule]]", model.Schedule);

            sb.Replace("[[Commercial]]", model.Brand);
            sb.Replace("[[BDT]]", model.BDT);
            sb.Replace("[[BDTs]]", model.BDTs);
            sb.Replace("[[Gross Total]]", model.BDT);
            sb.Replace("[[VAT]]", model.VAT);
            sb.Replace("[[Total]]", model.Total);
            sb.Replace("[[Less]]", model.Less);
            sb.Replace("[[Brand]]", model.Brand);
            sb.Replace("[[Type]]", "Branding");
            sb.Replace("[[ProgramName]]", model.TVCOrProduct); 
            sb.Replace("[[Episodes]]", model.Episodes);
            sb.Replace("[[Billing Invoice Detail]]", model.BillingDetail);

            //Report Header
            sb.Replace("[[TV Channel Logo]]", model.Logo);
            sb.Replace("[[TVChannel Address]]", model.Address);
            sb.Replace("[[Email]]", model.Email);
            sb.Replace("[[Contact]]", model.phone);
            sb.Replace("[[TVChannel Name]]", model.TVChannelName);

            return sb.ToString();
        }
    }
}

