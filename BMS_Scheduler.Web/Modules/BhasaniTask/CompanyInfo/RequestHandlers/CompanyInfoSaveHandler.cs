using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<BMS_Scheduler.BhasaniTask.CompanyInfoRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = BMS_Scheduler.BhasaniTask.CompanyInfoRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface ICompanyInfoSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class CompanyInfoSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ICompanyInfoSaveHandler
    {
        public CompanyInfoSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}