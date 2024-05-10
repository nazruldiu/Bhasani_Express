using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace BMS_Scheduler.BhasaniTask.Pages
{

    [PageAuthorize(typeof(CompanyInfoRow))]
    public class CompanyInfoController : Controller
    {
        [Route("BhasaniTask/CompanyInfo")]
        public ActionResult Index()
        {
            return View("~/Modules/BhasaniTask/CompanyInfo/CompanyInfoIndex.cshtml");
        }
    }
}