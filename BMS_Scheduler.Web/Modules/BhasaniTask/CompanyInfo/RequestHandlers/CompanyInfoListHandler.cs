using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<BMS_Scheduler.BhasaniTask.CompanyInfoRow>;
using MyRow = BMS_Scheduler.BhasaniTask.CompanyInfoRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface ICompanyInfoListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class CompanyInfoListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, ICompanyInfoListHandler
    {
        public CompanyInfoListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}