using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS_Scheduler.Common.Services;
using BMS_Scheduler.Web.Modules.Common.Business.Interfaces;
using BMS_Scheduler.Web.Modules.Common.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BMS_Scheduler
{
    public static class ServiceResolver
    {
        public static void Resolve(this IServiceCollection services)
        {

            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            #region service 
            services.AddTransient<IClientContractInformationService, ClientContractInformationService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClientContractInformationOtherTypeTVCRateService, ClientContractInformationOtherTypeTVCRateService>();
            services.AddTransient<IClientContractInformationPackageRateService, ClientContractInformationPackageRateService>();
            services.AddTransient<IClientContractInformationRegularTVCService, ClientContractInformationRegularTVCService>();
            services.AddTransient<ITimeSlotSegmentInformationDetailsService, TimeSlotSegmentInformationDetailsService>();
            //services.AddTransient<IBackgroundTaskManager, BackgroundTaskManager>();
            #endregion
        }
    }
}
