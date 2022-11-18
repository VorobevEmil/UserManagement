namespace UserManagement.Shared.Contracts.ManagementUser.Responses
{
    public class UserResponse
    {
        public string Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string StatusBlock { get; set; } = default!;
    }
}
