using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IShippingDeleteHandler
    {
        public ShippingDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}