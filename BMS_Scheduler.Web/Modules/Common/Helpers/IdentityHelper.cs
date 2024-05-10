using Microsoft.AspNetCore.Http;
using Serenity.Services;

namespace BMS_Scheduler.Web.Modules.Common
{
    public static class IdentityHelper
    {
        public static string GetClaimTypeByName(string ClaimName, IRequestContext Context = null)
        {
            var claimType = ClaimName;
            var claim = Context != null ? Context.User.FindFirst(claimType) : AppStatics.Context.User.FindFirst(claimType);
            var TvChannelId = claim == null ? string.Empty : claim.Value;

            return TvChannelId;
        }
    }
}
