using Serenity.ComponentModel;
using Serenity.Services;
using System.ComponentModel;

namespace BMS_Scheduler.Membership
{
    [FormScript("Membership.Login")]
    [BasedOnRow(typeof(Administration.Entities.UserRow), CheckNames = true)]
    public class LoginRequest : ServiceRequest
    {
        [Placeholder("user name"),DisplayName("User name")]
        public string Username { get; set; }
        [PasswordEditor, Required(true), Placeholder("password"), DisplayName("Password")]
        public string Password { get; set; }
    }
}