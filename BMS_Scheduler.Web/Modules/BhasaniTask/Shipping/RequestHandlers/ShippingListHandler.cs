using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<BMS_Scheduler.BhasaniTask.ShippingRow>;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IShippingListHandler
    {
        public ShippingListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}