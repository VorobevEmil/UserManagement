using UserManagement.Server.Models.DbModels;

namespace UserManagement.Server.Interfaces
{
    public interface IManagementUserService
    {
        Task<User> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
        Task RefreshStatusBlockAsync(bool refresh, string id, CancellationToken cancellationToken);
        Task SetLastLoginDateAsync(DateTime loginTime, string id, CancellationToken cancellationToken);
    }
}
