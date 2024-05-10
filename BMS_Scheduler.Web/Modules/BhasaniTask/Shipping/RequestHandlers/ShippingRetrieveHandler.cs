using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<BMS_Scheduler.BhasaniTask.ShippingRow>;
using MyRow = BMS_Scheduler.BhasaniTask.ShippingRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface IShippingRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ShippingRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IShippingRetrieveHandler
    {
        public ShippingRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}