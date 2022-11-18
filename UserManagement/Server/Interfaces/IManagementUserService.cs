using UserManagement.Server.Models.DbModels;
using UserManagement.Shared.Contracts.ManagementUser.Requests;

namespace UserManagement.Server.Interfaces
{
    public interface IManagementUserService
    {
        Task<User> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(string userId, CancellationToken cancellationToken);
        Task RefreshStatusBlockAsync(UserBlockRequest userBlockRequest, CancellationToken cancellationToken);
        Task SetLastLoginDateAsync(DateTime loginTime, string id, CancellationToken cancellationToken);
    }
}
