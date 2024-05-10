using Serenity.ComponentModel;
using System;
using System.Collections.Generic;

namespace BMS_Scheduler
{
    /// <summary>
    /// This data will be available from script code using a dynamic script.
    /// Add properties you need from script code and set them in UserEndpoint.GetUserData.
    /// </summary>
    [ScriptInclude]
    public class ScriptUserDefinition
    {
        public String Username { get; set; }
        public String DisplayName { get; set; }
        public Boolean IsAdmin { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
        public Int32 TVChannelId { get; set; }
        public Int32 UserId { get; set; }
    }
}