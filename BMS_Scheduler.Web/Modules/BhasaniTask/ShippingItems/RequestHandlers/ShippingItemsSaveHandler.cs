using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<BMS_Scheduler.BhasaniTask.ShippingItemsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingItemsRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingItemsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingItemsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IShippingItemsSaveHandler
    {
        public ShippingItemsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}