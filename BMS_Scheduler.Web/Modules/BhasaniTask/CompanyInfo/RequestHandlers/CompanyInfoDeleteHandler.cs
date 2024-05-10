using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = BMS_Scheduler.BhasaniTask.CompanyInfoRow;

namespace BMS_Scheduler.BhasaniTask
{
    public interface ICompanyInfoDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class CompanyInfoDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, ICompanyInfoDeleteHandler
    {
        public CompanyInfoDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}