using Serenity;
using Serenity.Web;
using Microsoft.AspNetCore.Mvc;

namespace BMS_Scheduler.BhasaniTask.Pages
{

    [PageAuthorize(typeof(ShippingItemsRow))]
    public class ShippingItemsController : Controller
    {
        [Route("BhasaniTask/ShippingItems")]
        public ActionResult Index()
        {
            return View("~/Modules/BhasaniTask/ShippingItems/ShippingItemsIndex.cshtml");
        }
    }
}