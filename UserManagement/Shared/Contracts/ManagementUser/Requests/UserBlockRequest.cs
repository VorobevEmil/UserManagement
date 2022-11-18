namespace UserManagement.Shared.Contracts.ManagementUser.Requests
{
    public class UserBlockRequest
    {
        public List<string> UsersId { get; set; } = default!;
        public bool StatusBlock { get; set; }
    }
}
