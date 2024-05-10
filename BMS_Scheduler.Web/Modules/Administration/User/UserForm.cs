using BMS_Scheduler.Common;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BMS_Scheduler.Administration.Forms
{
    [FormScript("Administration.User")]
    [BasedOnRow(typeof(Entities.UserRow), CheckNames = false)]
    public class UserForm
    {
        [HalfWidth(UntilNext = true)]
        public String Username { get; set; }
        public String DisplayName { get; set; }
        public String Email { get; set; }
        [PasswordEditor, Required(true)]
        public String Password { get; set; }
        [PasswordEditor, OneWay, Required(true)]
        public String PasswordConfirm { get; set; }

        [OneWay]
        public string Source { get; set; }
        public String UserImage { get; set; }
    }
}