using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingItemsRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingItemsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingItemsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IShippingItemsDeleteHandler
    {
        public ShippingItemsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}