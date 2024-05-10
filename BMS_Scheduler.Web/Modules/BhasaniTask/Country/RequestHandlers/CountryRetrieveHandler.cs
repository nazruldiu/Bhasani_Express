using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<BMS_Scheduler.BhasaniTask.CountryRow>;
using MyRow = BMS_Scheduler.BhasaniTask.CountryRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface ICountryRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class CountryRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ICountryRetrieveHandler
    {
        public CountryRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}