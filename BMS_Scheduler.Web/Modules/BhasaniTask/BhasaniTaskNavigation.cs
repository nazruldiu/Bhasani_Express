using Serenity.Navigation;
using MyPages = BMS_Scheduler.BhasaniTask.Pages;

[assembly: NavigationMenu(1, "Bhasani Task", icon: "fa-wrench")]
[assembly: NavigationLink(2, "Bhasani Task/Company Info", typeof(MyPages.CompanyInfoController), icon: "fa-circle-o")]
[assembly: NavigationLink(3, "Bhasani Task/Country", typeof(MyPages.CountryController), icon: "fa-circle-o")]
[assembly: NavigationLink(4, "Bhasani Task/Shipping", typeof(MyPages.ShippingController), icon: "fa-circle-o")]
//[assembly: NavigationLink(int.MaxValue, "Bhasani Task/Shipping Items", typeof(MyPages.ShippingItemsController), icon: null)]