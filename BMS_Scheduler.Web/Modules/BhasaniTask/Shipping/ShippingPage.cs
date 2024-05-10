using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace BMS_Scheduler.BhasaniTask.Pages
{

    [PageAuthorize(typeof(ShippingRow))]
    public class ShippingController : Controller
    {
        [Route("BhasaniTask/Shipping")]
        public ActionResult Index()
        {
            return View("~/Modules/BhasaniTask/Shipping/ShippingIndex.cshtml");
        }
    }
}