using Microsoft.AspNetCore.Identity;

namespace UserManagement.Server.Models.DbModels
{
    public class User : IdentityUser
    {
        public bool IsBlocked { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
