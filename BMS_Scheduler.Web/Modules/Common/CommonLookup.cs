using BMS_Scheduler.Common;
using BMS_Scheduler.Web.Modules.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common
{

    //[LookupScript("Common.TimeSlotByDate", Permission = "?", Expiration = -1)]
    //public class TimeSlotByDateLookup : LookupScript
    //{
    //    public TimeSlotByDateLookup()
    //    {

    //        IdField = TimeSlotSegmentInformationDetailsRow.Fields.Id.PropertyName;
    //        TextField = TimeSlotSegmentInformationDetailsRow.Fields.LookupText.PropertyName;
    //    }

    //    protected override IEnumerable GetItems()
    //    {
    //        return null;
    //    }
    //}

    //[LookupScript("Common.ProgramNameByDate", Permission = "?", Expiration = -1)]
    //public class ProgramNameByDateLookup : LookupScript
    //{
    //    public ProgramNameByDateLookup()
    //    {
    //        IdField = FpcTimeSlotSegmentInformationDetailRow.Fields.ClientId.PropertyName;
    //        TextField = FpcTimeSlotSegmentInformationDetailRow.Fields.CompanyName.PropertyName;
    //    }

    //    protected override IEnumerable GetItems()
    //    {
    //        return null;
    //    }
    //}

    //[LookupScript("Common.TVChannel_Lookup", Permission = "?", Expiration = -1)]
    //public class TVChannelLookup : RowLookupScript<TVChannelBasicInfoRow>
    //{
    //    public TVChannelLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {

    //        IdField = TVChannelBasicInfoRow.Fields.Id.PropertyName;
    //        TextField = TVChannelBasicInfoRow.Fields.Name.PropertyName;
    //    }

    //    protected override void PrepareQuery(SqlQuery query)
    //    {
    //        base.PrepareQuery(query);

    //        var fld = TVChannelBasicInfoRow.Fields;
    //        query.OrderBy(fld.Name);

    //    }
    //}


    //[LookupScript("Common.RoSpotPosition_Lookup", Permission = "?", Expiration = -1)]
    //public class RoSpotPositionLookup : RowLookupScript<SpotPositionRow>
    //{
    //    public RoSpotPositionLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {

    //        IdField = SpotPositionRow.Fields.Id.PropertyName;
    //        TextField = SpotPositionRow.Fields.Name.PropertyName;
    //    }

    //    protected override void PrepareQuery(SqlQuery query)
    //    {
    //        base.PrepareQuery(query);
    //        var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(IdentityClaimType.TvChannelId.ToDescriptionString()));

    //        query.Where(SpotPositionRow.Fields.TvChannelId == tvChannelId);
    //    }
    //}

    //[LookupScript("Common.ProgramTimeSlot_Lookup", Permission = "?", Expiration = -1)]
    //public class ProgramTimeSlotLookup : RowLookupScript<TimeSlotSegmentInformationDetailsRow>
    //{
    //    public ISqlConnections SqlConnection;
    //    public ProgramTimeSlotLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {
    //        SqlConnection = sqlConnections;
    //        IdField = TimeSlotSegmentInformationDetailsRow.Fields.Id.PropertyName;
    //        TextField = TimeSlotSegmentInformationDetailsRow.Fields.LookupText.PropertyName;

    //    }

    //    protected override List<TimeSlotSegmentInformationDetailsRow> GetItems()
    //    {
    //        var list = new List<TimeSlotSegmentInformationDetailsRow>();
    //        var fld = TimeSlotSegmentInformationDetailsRow.Fields;
    //        var bmsUtility = new BmsUtility();
    //        var query = new SqlQuery()
    //            .From(fld)
    //            .Select(fld.Id, fld.ProgramBanner, fld.StartTime, fld.EndTime)
    //            .OrderBy(fld.StartTime);

    //        using (var connection = SqlConnection.NewByKey("Default"))
    //        {

    //            list = connection.Query<TimeSlotSegmentInformationDetailsRow>(query).ToList();

    //        }

    //        foreach (var item in list)
    //        {

    //            //StartTime
    //            string startTime = bmsUtility.GetTime(item.StartTime);

    //            //EndTime
    //            string endTime = bmsUtility.GetTime(item.EndTime);

    //            item.LookupText = $"{item.ProgramBanner} ({startTime} - {endTime})";
    //        }

    //        return list;
    //    }


    //}


    ////Use the Id of the Master entity which is the foreign key in the Details entity as the IdField in the //Lookup Class

    //[LookupScript("RoBookingDetailNew", Permission = "?")]
    //public class RoBookingDetailNewLookup : RowLookupScript<RoBookingDetailRow>
    //{
    //    public RoBookingDetailNewLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {
    //        IdField = RoBookingDetailRow.Fields.TvcId.PropertyName;
    //        TextField = RoBookingDetailRow.Fields.TVCName.PropertyName;
    //    }

    //    protected override void PrepareQuery(SqlQuery query)
    //    {
    //        //base.PrepareQuery(query);
    //        var fld = RoBookingDetailRow.Fields;

    //        query
    //        .Select(fld.TvcId, fld.TVCName);
    //        //.Where(new Criteria(fld.SiteId) == 17)
    //        //.Where(new Criteria(fld.Obsolete) == 0);
    //    }

    //    protected override void ApplyOrder(SqlQuery query)
    //    {
    //    }
    //}


    //[LookupScript("Common.TimeSlotSegmentInfoDetails_Lookup", Permission = "?", Expiration = -1)]
    //public class TimeSlotSegmentInfoDetailsLookup : RowLookupScript<TimeSlotSegmentInformationDetailsRow>
    //{
    //    public TimeSlotSegmentInfoDetailsLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {

    //        IdField = TimeSlotSegmentInformationDetailsRow.Fields.Id.PropertyName;
    //        TextField = TimeSlotSegmentInformationDetailsRow.Fields.NameField.PropertyName;
    //    }

    //    protected override void PrepareQuery(SqlQuery query)
    //    {
    //        base.PrepareQuery(query);

    //        var fld = TimeSlotSegmentInformationDetailsRow.Fields;
    //        query.OrderBy(fld.NameField);

    //    }
    //}

    //[LookupScript("Common.Clients_Lookup", Permission = "?", Expiration = -1)]
    //public class ClientsLookup : RowLookupScript<ClientsRow>
    //{
    //    public ISqlConnections SqlConnection;
    //    public ClientsLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {
    //        SqlConnection = sqlConnections;
    //        IdField = ClientsRow.Fields.Id.PropertyName;
    //        TextField = ClientsRow.Fields.CompanyName.PropertyName;
    //    }


    //    protected override List<ClientsRow> GetItems()
    //    {
    //        var list = new List<ClientsRow>();
    //        var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(IdentityClaimType.TvChannelId.ToDescriptionString()));

    //        var fld = ClientsRow.Fields;
    //        var query = new SqlQuery()
    //            .From(fld)
    //            .Where($"Coalesce({fld.IsProgram},0) = 0")
    //            .Where(fld.TvChannelId == tvChannelId)
    //            .Select(fld.Id, fld.CompanyName, fld.IsActive, fld.IsProgram)
    //            .OrderBy(fld.CompanyName);


    //        using (var connection = SqlConnection.NewByKey("Default"))
    //        {
    //            list = connection.Query<ClientsRow>(query).ToList();
    //        }

    //        return list;
    //    }
    //}

    //[LookupScript("Common.Program_Lookup", Permission = "?", Expiration = -1)]
    //public class ProgramLookup : RowLookupScript<ProgramsRow>
    //{
    //    public ISqlConnections SqlConnection;
    //    public ProgramLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {
    //        SqlConnection = sqlConnections;
    //        IdField = ClientsRow.Fields.Id.PropertyName;
    //        TextField = ClientsRow.Fields.CompanyName.PropertyName;
    //    }

    //    protected override List<ClientsRow> GetItems()
    //    {
    //        var list = new List<ClientsRow>();
    //        var tvChannelId = Convert.ToInt32(IdentityHelper.GetClaimTypeByName(IdentityClaimType.TvChannelId.ToDescriptionString()));

    //        var fld = ClientsRow.Fields;
    //        var query = new SqlQuery()
    //            .From(fld)
    //            .Where($"Coalesce({fld.IsProgram},0) = 1")
    //            .Where(fld.TvChannelId == tvChannelId)
    //            .Select(fld.Id, fld.CompanyName, fld.IsActive, fld.IsProgram)
    //            .OrderBy(fld.CompanyName);
    //        using (var connection = SqlConnection.NewByKey("Default"))
    //        {
    //            list = connection.Query<ClientsRow>(query).ToList();
    //        }

    //        return list;
    //    }
    //}

    //[LookupScript("Common.ClientTVC_Lookup", Permission = "?", Expiration = -1)]
    //public class ClientTVCLookup : RowLookupScript<ClientTvcRow>
    //{
    //    public ISqlConnections SqlConnection;
    //    public ClientTVCLookup(ISqlConnections sqlConnections) : base(sqlConnections)
    //    {
    //        SqlConnection = sqlConnections;
    //        IdField = ClientTvcRow.Fields.Id.PropertyName;
    //        TextField = ClientTvcRow.Fields.LookupText.PropertyName;
    //    }


    //    protected override List<ClientTvcRow> GetItems()
    //    {
    //        var list = new List<ClientTvcRow>();

    //        var fld = ClientTvcRow.Fields;
    //        var clientFld = ClientsRow.Fields.As("Client");
    //        var categoryFld = ProgramCategoryRow.Fields.As("ProgramCategory");
    //        var tvcTypeFld = TVCTypeRow.Fields.As("TvcType");

    //        //query.OrderBy(fld.Id)
    //        //    .Select(fld.Id, fld.TvcName);

    //        var query = new SqlQuery()
    //            .From(fld)
    //            .InnerJoin(clientFld, clientFld.Id == fld.ClientId)
    //            .InnerJoin(categoryFld, clientFld.ProgramCategoryId == categoryFld.Id)
    //            .LeftJoin(tvcTypeFld, fld.TvcTypeId == tvcTypeFld.Id)
    //            .Select(fld.Id, fld.LookupText, fld.ClientId, fld.TvcName, fld.Duration, fld.Frame, fld.BillableDuration, fld.BillableFrame)
    //            .Select(clientFld.ProgramCategoryId)
    //            .Select(categoryFld.IsCommercial, nameof(fld.ProgramCategoryIsCommercial))
    //            .Select(categoryFld.IsSupportingContent, nameof(fld.IsSupportingContent))
    //            .Select(categoryFld.ColorCode, nameof(fld.CategoryColorCode))
    //            .Select(categoryFld.ForeColor, nameof(fld.CategoryForeColor))
    //            .Select(tvcTypeFld.Name, nameof(fld.TvcTypeName))
    //            .Where(categoryFld.IsCommercial == 1)
    //            .OrderBy(fld.LookupText);

    //        using (var connection = SqlConnection.NewByKey("Default"))
    //        {
    //            list = connection.Query<ClientTvcRow>(query).ToList();
    //        }

    //        return list;
    //    }
    //}
}





