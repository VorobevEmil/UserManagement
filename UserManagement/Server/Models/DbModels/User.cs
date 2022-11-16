using Microsoft.AspNetCore.Identity;
using UserManagement.Shared.Enums;

namespace UserManagement.Server.Models.DbModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = default!;
        public UserStatus UserStatus { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
