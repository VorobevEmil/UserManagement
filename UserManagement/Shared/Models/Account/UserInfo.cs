using System.Security.Claims;

namespace UserManagement.Shared.Models.Account
{
    public class UserInfo
    {
        public string AuthenticationType { get; set; } = default!;
        public List<Tuple<string,string>> Claims { get; set; } = default!;
    }
}
