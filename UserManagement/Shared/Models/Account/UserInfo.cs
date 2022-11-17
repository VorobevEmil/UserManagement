using System.Security.Claims;

namespace UserManagement.Shared.Models.Account
{
    public class UserInfo
    {
        public string AuthenticationType { get; set; } = default!;
        public List<ApiClaim> Claims { get; set; } = default!;
    }
}
