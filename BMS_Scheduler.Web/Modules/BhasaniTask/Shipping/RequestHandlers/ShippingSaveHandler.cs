using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<BMS_Scheduler.BhasaniTask.ShippingRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IShippingSaveHandler
    {
        public ShippingSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}