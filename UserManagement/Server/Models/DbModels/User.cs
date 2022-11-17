using Microsoft.AspNetCore.Identity;
using UserManagement.Shared.Enums;

namespace UserManagement.Server.Models.DbModels
{
    public class User : IdentityUser
    {
        public UserStatus UserStatus { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
