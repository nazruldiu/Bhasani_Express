using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<BMS_Scheduler.BhasaniTask.ShippingItemsRow>;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingItemsRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingItemsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingItemsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IShippingItemsListHandler
    {
        public ShippingItemsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}