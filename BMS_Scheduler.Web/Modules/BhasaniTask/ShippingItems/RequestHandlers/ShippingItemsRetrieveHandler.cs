using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<BMS_Scheduler.BhasaniTask.ShippingItemsRow>;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingItemsRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingItemsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingItemsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IShippingItemsRetrieveHandler
    {
        public ShippingItemsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}