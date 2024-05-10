using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<BMS_Scheduler.BhasaniTask.CountryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = BMS_Scheduler.BhasaniTask.CountryRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface ICountrySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class CountrySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ICountrySaveHandler
    {
        public CountrySaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}