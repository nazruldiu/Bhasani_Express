//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Serenity.Abstractions;
//using Serenity.Data;
//using Serenity.Web;
//using System;

//namespace BMS_Scheduler
//{
//    public static class AppStatics
//    {
//        //public static ISqlConnections SqlConnections { get; set; }
//        public static int MaxFrame = 24;

//        //public static void InitializeServices(IServiceProvider services)
//        //{
//        //    SqlConnections = services.GetRequiredService<ISqlConnections>();
//        //}

//        public static ISqlConnections SqlConnections { get; set; }
//        public static ITwoLevelCache Cache { get; set; }
//        public static IUserRetrieveService UserRetrieveService { get; set; }
//        public static IConfiguration Configuration { get; set; }
//        public static IWebHostEnvironment HostEnvironment { get; set; }
//        public static IExceptionLogger ExceptionLogger { get; set; }
//        public static AppSettings Settings { get; set; }
//        public static UploadSettings UploadSettings { get; set; }

//        public static void InitilizeConfigurations(IConfiguration configuration, IWebHostEnvironment env)
//        {
//            Configuration = configuration;
//            HostEnvironment = env;

//            var appSettingsSection = configuration.GetSection(AppSettings.SectionKey);
//            Settings = new AppSettings
//            {
//                AppType = appSettingsSection.GetSection(nameof(AppSettings.AppType)).Value,
//                MainAppUrl = appSettingsSection.GetSection(nameof(AppSettings.MainAppUrl)).Value,
//                ServerName = appSettingsSection.GetSection(nameof(AppSettings.ServerName)).Value,
//            };

//            var uploadSettingsSection = configuration.GetSection(UploadSettings.SectionKey);
//            UploadSettings = new UploadSettings
//            {
//                Path = uploadSettingsSection.GetSection(nameof(UploadSettings.Path)).Value
//            };
//        }

//        //public static void ConfigureServices(IServiceCollection services)
//        //{
//        //    services.AddSingleton<IConnectionStrings, MyConnectionStrings>();
//        //}

//        public static void InitializeServices(IServiceProvider services)
//        {
//            SqlConnections = services.GetRequiredService<ISqlConnections>();
//            Cache = services.GetRequiredService<ITwoLevelCache>();
//            UserRetrieveService = services.GetRequiredService<IUserRetrieveService>();
//            ExceptionLogger = null; //services.GetRequiredService<IExceptionLogger>();

//            //if (SqlConnections.TryGetConnectionString("Default").ProviderName == "Oracle.ManagedDataAccess.Client")
//            //{
//            //    SqlSettings.DefaultDialect = Oracle12cDialect.Instance;
//            //}
//        }


//    }
//}
