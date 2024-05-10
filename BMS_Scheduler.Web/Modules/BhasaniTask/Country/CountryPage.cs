using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace BMS_Scheduler.BhasaniTask.Pages
{

    [PageAuthorize(typeof(CountryRow))]
    public class CountryController : Controller
    {
        [Route("BhasaniTask/Country")]
        public ActionResult Index()
        {
            return View("~/Modules/BhasaniTask/Country/CountryIndex.cshtml");
        }
    }
}