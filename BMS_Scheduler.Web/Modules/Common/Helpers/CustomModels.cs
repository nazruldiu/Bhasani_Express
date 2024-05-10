using Serenity.Data.Mapping;
using System;

namespace BMS_Scheduler.Web.Modules.Common.Helpers
{
    public class GPRPTimeListModel
    {
        public int startTime { get; set; }
        public int endTime { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string startTimeEndTimeString { get; set; }
    }
}
