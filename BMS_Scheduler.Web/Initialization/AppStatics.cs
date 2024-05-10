using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serenity.Abstractions;
using Serenity.Data;
using Serenity.Services;
using Serenity.Web;
using System;

namespace BMS_Scheduler
{
    public static class AppStatics
    {
        public static int MaxFrame = 25;
        public static int MaxSecond = 60;
        public static ISqlConnections SqlConnections { get; set; }
        public static ITwoLevelCache Cache { get; set; }
        public static IUserRetrieveService UserRetrieveService { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static IWebHostEnvironment HostEnvironment { get; set; }
        public static IExceptionLogger ExceptionLogger { get; set; }
        public static AppSettings Settings { get; set; }
        public static UploadSettings UploadSettings { get; set; }
        public static IRequestContext Context { get; set; }

        public static void InitilizeConfigurations(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;

            var appSettingsSection = configuration.GetSection(AppSettings.SectionKey);
            Settings = new AppSettings
            {
                AppType = appSettingsSection.GetSection(nameof(AppSettings.AppType)).Value,
                MainAppUrl = appSettingsSection.GetSection(nameof(AppSettings.MainAppUrl)).Value,
                ServerName = appSettingsSection.GetSection(nameof(AppSettings.ServerName)).Value,
            };

            var uploadSettingsSection = configuration.GetSection(UploadSettings.SectionKey);
            UploadSettings = new UploadSettings
            {
                Path = uploadSettingsSection.GetSection(nameof(UploadSettings.Path)).Value
            };
        }

        public static void InitializeServices(IServiceProvider services)
        {
            SqlConnections = services.GetRequiredService<ISqlConnections>();
            //Cache = services.GetRequiredService<ITwoLevelCache>();
            UserRetrieveService = services.GetRequiredService<IUserRetrieveService>();
            //ExceptionLogger = null; //services.GetRequiredService<IExceptionLogger>();
            Context = services.GetRequiredService<IRequestContext>();

            if (SqlConnections.TryGetConnectionString("Default").ProviderName == "Oracle.ManagedDataAccess.Client")
            {
                SqlSettings.DefaultDialect = Oracle12cDialect.Instance;
            }
        }
    }

    public class AppSettings
    {
        public const string SectionKey = "AppSettings";

        public string AppType { get; set; } //BackOffice, NewConnectionApplication, BankCollection, EPaymentAPI
        public string MainAppUrl { get; set; }
        public string ServerName { get; set; }
    }
}
