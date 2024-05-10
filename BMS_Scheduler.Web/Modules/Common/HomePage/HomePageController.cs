using Microsoft.AspNetCore.Mvc;

namespace BMS_Scheduler.Web.Modules.Common.HomePage
{
    public class HomePageController : Controller
    {
        [Route("~/")]
        public IActionResult Index()
        {
            return View(MVC.Views.Common.HomePage.HomePageIndex);
        }
    }
}
