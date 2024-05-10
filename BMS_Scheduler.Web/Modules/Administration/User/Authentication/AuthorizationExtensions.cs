using Serenity;
using Serenity.Services;
using System;

namespace BMS_Scheduler
{
    public static class AuthorizationExtensions
    {
        public static UserDefinition GetUserDefinition(this IRequestContext requestContext)
        {
            if (requestContext?.User?.Identity?.Name is null) return null;

            return AppStatics.UserRetrieveService.ByUsername(requestContext.User.Identity.Name) as UserDefinition;
        }
    }
}
