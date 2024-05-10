using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace BMS_Scheduler.Administration.Forms
{
    [ColumnsScript("Administration.Role")]
    [BasedOnRow(typeof(Entities.RoleRow), CheckNames = true)]
    public class RoleColumns
    {
        [EditLink, Width(300)]
        public String RoleName { get; set; }
    }
}