using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<BMS_Scheduler.BhasaniTask.CompanyInfoRow>;
using MyRow = BMS_Scheduler.BhasaniTask.CompanyInfoRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface ICompanyInfoRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class CompanyInfoRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ICompanyInfoRetrieveHandler
    {
        public CompanyInfoRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}