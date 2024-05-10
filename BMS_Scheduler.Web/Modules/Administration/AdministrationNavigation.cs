using Serenity.Navigation;
using MyPages = BMS_Scheduler.Administration.Pages;
using Administration = BMS_Scheduler.Administration.Pages;

[assembly: NavigationMenu(1, "Administration", icon: "fa-wrench")]
//[assembly: NavigationLink(9100, "Administration/Languages", typeof(Administration.LanguageController), icon: "fa-comments")]
//[assembly: NavigationLink(9200, "Administration/Translations", typeof(Administration.TranslationController), icon: "fa-comment-o")]
[assembly: NavigationLink(2, "Administration/Roles", typeof(Administration.RoleController), icon: "fa-lock")]
[assembly: NavigationLink(4, "Administration/User Management", typeof(Administration.UserController), icon: "fa-users")]
[assembly: NavigationLink(5, "Administration/Audit Log", typeof(_Ext.Pages.AuditLogController), icon: "fa-history")]
