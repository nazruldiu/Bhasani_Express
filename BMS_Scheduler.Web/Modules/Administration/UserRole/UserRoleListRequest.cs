using Serenity.Services;

namespace BMS_Scheduler.Administration
{
    public class UserRoleListRequest : ServiceRequest
    {
        public int? UserID { get; set; }
    }
}