using System;

namespace BMS_Scheduler.Web.Modules.Common.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public string IUser { get; set; }
        public string EUser { get; set; }
        public DateTime IDate { get; set; }
        public DateTime EDate { get; set; }
    }
}
